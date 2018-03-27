using ConsoleApp6.Templates.CSharpTypeMembers;
using System.Collections.Generic;

namespace ConsoleApp6.Templates.CodeGenDeclarations.RepositoryGroups
{
    public interface IRepositoryGroup
    {
        string Description { get; set; }
        string Name { get; set; }
        List<PackageReference> PackageReferences { get; set; }
        List<string> SameAccountDependencies { get; set; }
        string Version { get; set; }
        IEnumerable<ICodeGeneratable> GetRepositories();
    }
}
