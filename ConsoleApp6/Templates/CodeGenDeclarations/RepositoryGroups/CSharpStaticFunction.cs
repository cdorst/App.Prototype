using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public class CSharpInterface : IRepositoryGroup
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<PackageReference> PackageReferences { get; set; }
        public List<string> SameAccountDependencies { get; set; }
        public string Version { get; set; }

        public List<Attribute> Attributes { get; set; }
        public List<Base> Bases { get; set; }
        public List<ConstraintClause> ConstraintClauses { get; set; }
        public List<Method> Methods { get; set; }
        public List<Property> Properties { get; set; }
        public List<string> TypeParameters { get; set; }
        public List<string> UsingDirectives { get; set; }

        public IEnumerable<ICodeGeneratable> GetRepositories()
        {
            yield return new Interface(Name, Description, Version, PackageReferences, SameAccountDependencies, Attributes, Bases, ConstraintClauses, Methods, Properties, TypeParameters, UsingDirectives);
        }
    }

    public class Entity : IRepositoryGroup
    {
        public string DependsOn { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<PackageReference> PackageReferences { get; set; }
        public List<EntityProperty> Properties { get; set; }
        public List<string> SameAccountDependencies { get; set; }
        public string Version { get; set; }

        public bool? Editable { get; set; }
        public int EntityTypeId { get; set; }
        public string KeyType { get; set; }

        public IEnumerable<ICodeGeneratable> GetRepositories()
        {
            if (!string.IsNullOrEmpty(DependsOn))
            {
                if (SameAccountDependencies == null) SameAccountDependencies = new List<string>();
                if (!SameAccountDependencies.Contains(DependsOn)) SameAccountDependencies.Add(DependsOn);
            }

            var tableName = Name.Split('.').Last(); // Foo.Bars => Bars
            var typeName = tableName.Singularize(); // Bars => Bar
            yield return EntityTypeBuilder.Build(Name, typeName, Description, Version, tableName, PackageReferences, SameAccountDependencies, KeyType, Properties, @static: !(Editable ?? false), EntityTypeId);
        }
    }

    public class EntityProperty
    {
        public string ForeignKeyType { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Type of value-type or foreign-key target entity
        /// </summary>
        public string Type { get; set; }
        public string TypeNamespace { get; set; }
    }

    internal static class EntityNamespaceConstants
    {
        public const string ProtobufPosition = "Position = Protobuf.ProtoMember";
        public const string ProtobufSerializable = "ProtobufSerializable = Protobuf.ProtoContract";
        public const string System = nameof(System);
        public const string SystemComponentModelDataAnnotations = "System.ComponentModel.DataAnnotations";
        public const string SystemComponentModelDataAnnotationsSchema = "System.ComponentModel.DataAnnotations.Schema";
        public const string SystemLinqExpressions = "System.Linq.Expressions";
    }

    public static class EntityTypeBuilder
    {
        private const string dot = ".";
        private const string GetEntityTypeId = nameof(GetEntityTypeId);
        private const string GetEntityTypeIdComment = "Returns a value that uniquely identifies this entity type. Each entity type in the model has a unique identifier.";
        private const string IEntity = nameof(IEntity);
        private const string IStaticEntity = nameof(IStaticEntity);
        private const string Key = nameof(Key);
        private const string ProtoContract = nameof(ProtoContract);
        private const string Position = nameof(Position);
        private const string PositionKeyExpression = "(1)";
        private const string Table = nameof(Table);

        private static readonly System.Func<string, string> Alphabetical = @string => @string;
        private static readonly List<Attribute> KeyAttributes = new List<Attribute>
        {
            new Attribute(Key),
            new Attribute(Position, PositionKeyExpression)
        };
        private static readonly Constructor DefaultConstructor = new Constructor();
        private static readonly System.Func<string, string> PropertyAssignmentStatement = name => $"{name} = {name[0].ToString().ToLower()}{name.Substring(1)}";
        private static readonly System.Func<EntityProperty, string> PropertyName = property => property.Name;
        private static readonly System.Func<EntityProperty, Parameter> PropertyParameter = property => new Parameter(property.Name, property.Type);
        private static readonly System.Func<EntityProperty, string> PropertyTypeNamespace = property => property.TypeNamespace;
        private static readonly System.Func<EntityProperty, bool> PropertyHasTypeNamespace = property => !string.IsNullOrEmpty(property.TypeNamespace);
        private static readonly Attribute ProtoContractAttribute = new Attribute(ProtoContract);

        public static Class Build(string @namespace, string typeName, string description, string version, string tableName, List<PackageReference> packageReferences, List<string> sameAccountDependencies, string keyType, List<EntityProperty> properties, bool @static, int entityTypeId)
        {
            if (string.IsNullOrWhiteSpace(keyType)) keyType = TypeConstants.Int;
            return new Class(@namespace, typeName, description, version, packageReferences, sameAccountDependencies, GetAttributes(tableName, @namespace), GetBases(@static, keyType), GetConstructors(properties), methods: GetMethods(@static, typeName, keyType, properties, entityTypeId), properties: GetProperties(typeName, keyType, properties).ToList(), usingDirectives: GetUsingDirectives(properties));
        }

        private static List<Method> GetMethods(bool @static, string typeName, string keyType, List<EntityProperty> properties, int entityTypeId)
        {
            var methods = new List<Method>
            {
                new Method(GetEntityTypeId, TypeConstants.Int, GetEntityTypeIdComment, entityTypeId.ToString())
            };
            if (@static)
            {

            }
            return methods;
        }

        private static List<string> GetUsingDirectives(List<EntityProperty> properties)
        {
            var usings = new HashSet<string>(properties
                .Where(PropertyHasTypeNamespace)
                .Select(PropertyTypeNamespace)
                .Distinct());
            usings.Add(EntityNamespaceConstants.ProtobufPosition);
            usings.Add(EntityNamespaceConstants.ProtobufSerializable);
            usings.Add(EntityNamespaceConstants.System);
            usings.Add(EntityNamespaceConstants.SystemComponentModelDataAnnotations);
            usings.Add(EntityNamespaceConstants.SystemComponentModelDataAnnotationsSchema);
            usings.Add(EntityNamespaceConstants.SystemLinqExpressions);
            return usings.OrderBy(Alphabetical).ToList();
        }

        private static IEnumerable<Property> GetProperties(string typeName, string keyType, List<EntityProperty> properties)
        {
            yield return new Property($"{typeName}Id", keyType, $"{typeName} unique identifier (primary key)", attributes: KeyAttributes);
            byte protobufPosition = 2;
            foreach (var property in properties.OrderBy(PropertyName))
            {
                var fk = property.ForeignKeyType;
                var name = property.Name;
                if (string.IsNullOrEmpty(fk))
                {
                    yield return new Property(name, property.Type, $"Contains {name} value",
                        attributes: GetPropertyAttributes(protobufPosition));
                }
                else
                {
                    yield return new Property($"{name}Id", fk, $"Contains {name} foreign key",
                        attributes: GetPropertyAttributes(protobufPosition));
                    protobufPosition++;
                    yield return new Property(name, property.Type, $"Contains {name} reference",
                        attributes: GetPropertyAttributes(protobufPosition));
                }
                protobufPosition++;
            }
        }

        private static List<Attribute> GetPropertyAttributes(byte protobufPosition)
            => new List<Attribute>
            {
                new Attribute(Position, $"({protobufPosition})")
            };

        private static List<Attribute> GetAttributes(string tableName, string @namespace)
            => new List<Attribute>
            {
                ProtoContractAttribute,
                new Attribute(Table,
                    $"(\"{tableName}\", Schema = \"{@namespace.Replace(dot, string.Empty)}\")")
            };

        private static List<Base> GetBases(bool @static, string keyType)
            => new List<Base>
            {
                new Base(@static ? IStaticEntity : IEntity, keyType)
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
                Parameters = GetConstructorWithAssignmentsParameters(properties)
            };

        private static List<string> GetConstructorWithAssignmentsBlock(List<EntityProperty> properties)
            => properties
                .Select(PropertyName)
                .OrderBy(Alphabetical)
                .Select(PropertyAssignmentStatement)
                .ToList();

        private static List<Parameter> GetConstructorWithAssignmentsParameters(List<EntityProperty> properties)
            => properties.Select(PropertyParameter).ToList();
    }

    public class CSharpStaticFunction : IRepositoryGroup
    {
        public string Description { get; set; }
        List<EnvironmentVariable> EnvironmentVariables { get; set; }
        public List<Field> Fields { get; set; }
        public string Name { get; set; }
        public List<PackageReference> PackageReferences { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string ReturnType { get; set; }
        public List<string> SameAccountDependencies { get; set; }
        public List<string> Statements { get; set; }
        public List<string> TypeParameters { get; set; }
        public List<string> UsingDirectives { get; set; }
        public List<string> UsingStaticDirectives { get; set; }
        public string Version { get; set; }

        public IEnumerable<ICodeGeneratable> GetRepositories()
        {
            var isAsync = IsFunctionAsync();
            var arrowClause = Statements.Count > 1 ? null
                : Statements.First();
            if (arrowClause != null && arrowClause.StartsWith("return "))
                arrowClause = arrowClause.Replace("return ", string.Empty);
            var block = arrowClause != null ? null : Statements;
            var names = GetTypeAndMethodNames();
            var methods = new List<Method>
            {
                new Method
                {
                    ArrowClauseExpression = arrowClause,
                    Block = block,
                    Comment = Description,
                    Modifiers = isAsync
                        ? "public static async"
                        : "public static",
                    Name = names.Last(),
                    Parameters = Parameters,
                    Type = ReturnType,
                    TypeParameters = TypeParameters
                }
            };
            yield return new StaticFunction(
                Name, names.First(), Description, Version, EnvironmentVariables, PackageReferences, SameAccountDependencies, fields: Fields, methods: methods, usingDirectives: UsingDirectives, usingStaticDirectives: UsingStaticDirectives);
        }

        private IEnumerable<string> GetTypeAndMethodNames()
        {
            // "Me.MyProduct.GetConnection" => "ConnectionGetter"
            var words = Name
                .Split('.').Last() // => "GetConnection"
                .Humanize(LetterCasing.Title) // => "Get Connection"
                .Split(' ');
            var agentNoun = words.First() // => "Get"
                .GetAgentNoun(); // => "Getter
            var methodName = string.Join(string.Empty, words.Skip(1));
            yield return $"{methodName}{agentNoun}"; // "ConnectionGetter"
            yield return methodName; // "Connection"
        }

        private bool IsFunctionAsync()
            => ReturnType == "Task" || ReturnType.StartsWith("Task<");
    }
}
