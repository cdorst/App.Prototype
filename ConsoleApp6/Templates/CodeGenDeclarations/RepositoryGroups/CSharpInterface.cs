using ConsoleApp6.Templates.CSharpTypeMembers;
using System.Collections.Generic;

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
}
