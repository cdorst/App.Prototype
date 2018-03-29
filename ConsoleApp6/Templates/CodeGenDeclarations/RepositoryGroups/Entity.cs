using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.CSharp.Helpers.Common;
using Humanizer;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
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
            var tableName = Name.Split('.').Last(); // Foo.Bars => Bars
            var typeName = tableName.Singularize(); // Bars => Bar
            if (string.IsNullOrWhiteSpace(KeyType)) KeyType = TypeConstants.Int;
            var @static = !(Editable ?? false);
            yield return EntityTypeBuilder.Build(Name, typeName, Description, Version, tableName, PackageReferences, SameAccountDependencies, KeyType, Properties, @static, EntityTypeId);
            yield return EntityDbContextBuilder.Build(Name, typeName, Version, tableName, @static, DependsOn);
            yield return EntityApiControllerBuilder.Build(Name, typeName, Version, KeyType, tableName, @static);
        }
    }
}
