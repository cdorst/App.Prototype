using ConsoleApp6.Templates.CSharpTypeMembers;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6.Templates.CodeGenDeclarations
{
    public class Metapackage : ICodeGeneratable
    {
        public Metapackage() { }
        public Metapackage(string name, string description, string version, List<EnvironmentVariable> environmentVariables = null, List<PackageReference> externalDependencies = null, params string[] sameAccountDependencies)
        {
            Description = description;
            EnvironmentVariables = environmentVariables;
            Name = name;
            PackageReferences = externalDependencies;
            SameAccountDependencies = sameAccountDependencies?.ToList();
            Version = version;
        }

        public string Description { get; set; }
        public List<EnvironmentVariable> EnvironmentVariables { get; set; }
        public string Name { get; set; }
        public List<PackageReference> PackageReferences { get; set; }
        public List<string> SameAccountDependencies { get; set; }
        public string Version { get; set; }

        public RepositoryDeclaration GetDeclaration()
            => new NuGetMetapackage(Name, Description, Version, EnvironmentVariables, PackageReferences, SameAccountDependencies.ToArray());
    }
}
