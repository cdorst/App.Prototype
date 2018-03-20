using ConsoleApp6.Templates.CSharpTypeMembers;
using System;
using System.Collections.Generic;
using static DevOps.Primitives.SourceGraph.Helpers.Consolidated.Builders.MetapackageBuilder;

namespace ConsoleApp6.Templates
{
    public class NuGetMetapackage : RepositoryDeclaration
    {
        public NuGetMetapackage() { }
        public NuGetMetapackage(string name, string description, string version, List<EnvironmentVariable> environmentVariables = null, List<PackageReference> externalDependencies = null, params string[] sameAccountDependencies) : base(name, description, version, environmentVariables, externalDependencies, sameAccountDependencies)
        {
        }

        public override Func<string, IDictionary<string, ITemplate>, TemplateContent> GetContent()
            => (accountName, repositories)
                => new TemplateContent(Metapackage(
                    Name, Description, Version, GetPackages(repositories, accountName), GetEnvironmentVariables()));
    }
}
