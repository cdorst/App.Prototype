using System.Collections.Generic;

namespace ConsoleApp6.Templates.CodeGenDeclarations
{
    public interface ICodeGeneratable
    {
        string Description { get; set; }
        List<CSharpTypeMembers.EnvironmentVariable> EnvironmentVariables { get; set; }
        string Name { get; set; }
        List<CSharpTypeMembers.PackageReference> PackageReferences { get; set; }
        List<string> SameAccountDependencies { get; set; }
        string Version { get; set; }

        RepositoryDeclaration GetDeclaration();
    }
}
