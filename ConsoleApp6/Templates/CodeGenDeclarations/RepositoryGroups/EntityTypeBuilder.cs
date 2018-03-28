using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public static class EntityTypeBuilder
    {
        private const string AnnotationsPackage = "DevOps.Code.Entities.Metapackages.Annotations";
        private const string dot = ".";
        private const string GetEntityType = nameof(GetEntityType);
        private const string GetEntityTypeIdComment = "Returns a value that uniquely identifies this entity type. Each entity type in the model has a unique identifier";
        private const string GetKey = nameof(GetKey);
        private const string GetKeyComment = "Returns the entity's unique identifier";
        private const string GetUniqueIndex = nameof(GetUniqueIndex);
        private const string GetUniqueIndexComment = "Returns an expression defining this entity's unique index (alternate key)";
        private const string IEntity = nameof(IEntity);
        private const string IEntityPackage = "DevOps.Code.Entities.Interfaces.Entity";
        private const string IStaticEntity = nameof(IStaticEntity);
        private const string IStaticEntityPackage = "DevOps.Code.Entities.Interfaces.StaticEntity";
        private const string Key = nameof(Key);
        private const string Position = nameof(Position);
        private const string PositionKeyExpression = "(1)";
        private const string Table = nameof(Table);

        private static readonly System.Func<string, string> Alphabetical = @string => @string;
        private static readonly List<Attribute> KeyAttributes = new List<Attribute>
        {
            new Attribute(Key),
            new Attribute(Position, PositionKeyExpression)
        };
        private static readonly Constructor DefaultConstructor = new Constructor() { Modifiers = "public" };
        private static readonly System.Func<string, string> PropertyAssignmentStatement = name => $"{name} = {name.ToCamelCase()};";
        private static readonly System.Func<EntityProperty, string> PropertyName = property => property.Name;
        private static readonly System.Func<EntityProperty, Parameter> PropertyParameter = property => new Parameter(property.Name.ToCamelCase(), property.Type);
        private static readonly System.Func<EntityProperty, string> PropertyTypeNamespace = property => property.TypeNamespace;
        private static readonly System.Func<EntityProperty, bool> PropertyHasTypeNamespace = property => !string.IsNullOrEmpty(property.TypeNamespace);
        private static readonly Attribute ProtobufSerializableAttribute = new Attribute(nameof(EntityNamespaceConstants.ProtoBufSerializable));
        private static readonly System.Func<EntityProperty, string> ValueOrForeignKey = p => string.IsNullOrEmpty(p.ForeignKeyType) ? p.Name : $"{p.Name}Id";

        public static Class Build(string @namespace, string typeName, string description, string version, string tableName, List<PackageReference> packageReferences, List<string> sameAccountDependencies, string keyType, List<EntityProperty> properties, bool @static, int entityTypeId)
        {
            if (string.IsNullOrWhiteSpace(keyType)) keyType = TypeConstants.Int;
            sameAccountDependencies = BuildSameAccountDependencyList(sameAccountDependencies, @static);
            return new Class(@namespace, typeName, description, version, packageReferences, sameAccountDependencies, GetAttributes(tableName, @namespace), GetBases(@static, keyType, typeName), GetConstructors(properties), methods: GetMethods(@static, typeName, keyType, properties, entityTypeId), properties: GetProperties(typeName, keyType, properties).ToList(), usingDirectives: GetUsingDirectives(properties, @static));
        }

        private static List<string> BuildSameAccountDependencyList(List<string> dependencies, bool @static)
        {
            if (dependencies == null)
                dependencies = new List<string>();
            if (!dependencies.Contains(AnnotationsPackage))
                dependencies.Add(AnnotationsPackage);
            if (@static)
            {
                if (!dependencies.Contains(IStaticEntityPackage))
                    dependencies.Add(IStaticEntityPackage);
            }
            else
            {
                if (!dependencies.Contains(IEntityPackage))
                    dependencies.Add(IEntityPackage);
            }
            return dependencies;
        }

        private static List<Attribute> GetAttributes(string tableName, string @namespace)
            => new List<Attribute>
            {
                ProtobufSerializableAttribute,
                new Attribute(Table,
                    $"(\"{tableName}\", Schema = \"{GetAttributeSchemaName(@namespace)}\")")
            };

        private static string GetAttributeSchemaName(string @namespace)
        {
            var nameParts = @namespace.Split(dot);
            return string.Join(string.Empty, nameParts.Take(nameParts.Length - 1));
        }

        private static List<Base> GetBases(bool @static, string keyType, string typeName)
            => new List<Base>
            {
                @static
                    ? new Base(IStaticEntity, typeName, keyType)
                    : new Base(IEntity, keyType)
            };

        private static List<Constructor> GetConstructors(List<EntityProperty> properties)
            => new List<Constructor>
            {
                DefaultConstructor,
                GetConstructorWithAssignments(properties)
            };

        private static Constructor GetConstructorWithAssignments(List<EntityProperty> properties)
            => new Constructor
            {
                Block = GetConstructorWithAssignmentsBlock(properties),
                Parameters = GetConstructorWithAssignmentsParameters(properties),
                Modifiers = "public"
            };

        private static List<string> GetConstructorWithAssignmentsBlock(List<EntityProperty> properties)
            => properties
                .Select(PropertyName)
                .OrderBy(Alphabetical)
                .Select(PropertyAssignmentStatement)
                .ToList();

        private static List<Parameter> GetConstructorWithAssignmentsParameters(List<EntityProperty> properties)
            => properties.Select(PropertyParameter).ToList();

        private static List<Method> GetMethods(bool @static, string typeName, string keyType, List<EntityProperty> properties, int entityTypeId)
        {
            var methods = new List<Method>
            {
                new Method(GetEntityType, TypeConstants.Int, GetEntityTypeIdComment, entityTypeId.ToString()),
                new Method(GetKey, keyType, GetKeyComment, $"{typeName}Id")
            };
            if (@static)
            {
                var uniqueProperties = properties
                    .Select(ValueOrForeignKey)
                    .OrderBy(Alphabetical);
                var uniquePropertyString = string.Join(", entity.", uniqueProperties);
                methods.Add(new Method(GetUniqueIndex, $"Expression<Func<{typeName}, object>>", GetUniqueIndexComment, $"entity => new {{ entity.{uniquePropertyString} }}"));
            }
            return methods;
        }

        private static IEnumerable<Property> GetProperties(string typeName, string keyType, List<EntityProperty> properties)
        {

            yield return new Property($"{typeName}Id", keyType, $"{typeName} unique identifier (primary key)", attributes: KeyAttributes, modifiers: "public");
            byte protobufPosition = 2;
            foreach (var property in properties.OrderBy(PropertyName))
            {
                var fk = property.ForeignKeyType;
                var name = property.Name;
                if (string.IsNullOrEmpty(fk))
                {
                    yield return new Property(name, property.Type, $"Contains {name} value", attributes: GetPropertyAttributes(protobufPosition), modifiers: "public");
                }
                else
                {
                    yield return new Property($"{name}Id", fk, $"Contains {name} foreign key", attributes: GetPropertyAttributes(protobufPosition), modifiers: "public");
                    protobufPosition++;
                    yield return new Property(name, property.Type, $"Contains {name} reference", attributes: GetPropertyAttributes(protobufPosition), modifiers: "public");
                }
                protobufPosition++;
            }
        }

        private static List<Attribute> GetPropertyAttributes(byte protobufPosition)
            => new List<Attribute>
            {
                new Attribute(Position, $"({protobufPosition})")
            };

        private static List<string> GetUsingDirectives(List<EntityProperty> properties, bool @static)
        {
            var usings = new HashSet<string>(properties
                .Where(PropertyHasTypeNamespace)
                .Select(PropertyTypeNamespace)
                .Distinct());
            if (@static)
            {
                usings.Add(EntityNamespaceConstants.DevOpsCodeEntitiesInterfacesStaticEntity);
            }
            else
            {
                usings.Add(EntityNamespaceConstants.DevOpsCodeEntitiesInterfacesEntity);
            }
            usings.Add(EntityNamespaceConstants.ProtoBufPosition);
            usings.Add(EntityNamespaceConstants.ProtoBufSerializable);
            usings.Add(EntityNamespaceConstants.System);
            usings.Add(EntityNamespaceConstants.SystemComponentModelDataAnnotations);
            usings.Add(EntityNamespaceConstants.SystemComponentModelDataAnnotationsSchema);
            usings.Add(EntityNamespaceConstants.SystemLinqExpressions);
            return usings.OrderBy(Alphabetical).ToList();
        }
    }
}
