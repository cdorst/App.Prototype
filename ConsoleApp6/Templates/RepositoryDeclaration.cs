using ConsoleApp6.Templates.CSharpTypeMembers;
using DevOps.Primitives.NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;

namespace ConsoleApp6.Templates
{
    public abstract class RepositoryDeclaration : ITemplate
    {
        private static readonly List<NuGetReference> Empty = new List<NuGetReference>();

        public RepositoryDeclaration() { }
        public RepositoryDeclaration(string name, string description, string version, List<EnvironmentVariable> environmentVariables, List<PackageReference> externalDependencies, IEnumerable<string> sameAccountDependencies)
        {
            Description = description;
            EnvironmentVariables = environmentVariables;
            ExternalDependencies = externalDependencies;
            Name = name;
            SameAccountDependencies = sameAccountDependencies;
            Version = version;
        }

        public string Description { get; set; }
        public List<EnvironmentVariable> EnvironmentVariables { get; set; }
        public List<PackageReference> ExternalDependencies { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> SameAccountDependencies { get; set; }
        public string Version { get; set; }

        public abstract Func<string, IDictionary<string, ITemplate>, TemplateContent> GetContent();

        protected IDictionary<string, string> GetEnvironmentVariables()
            => !Any(EnvironmentVariables) ? null
                : new Dictionary<string, string>(EnvironmentVariables.Select(each => new KeyValuePair<string, string>(each.Name, each.Description)));

        protected List<NuGetReference> GetPackages(IDictionary<string, ITemplate> repositories, string prefix = null)
            => GetExternalPackages()
                .Concat(GetInternalPackages(repositories, prefix))
                .ToList();

        private IEnumerable<NuGetReference> GetExternalPackages()
             => ExternalDependencies?.Select(each
                 => new NuGetReference(each.Name, each.Version)) ?? Empty;

        private IEnumerable<NuGetReference> GetInternalPackages(IDictionary<string, ITemplate> repositories, string prefix)
        {
            if (!string.IsNullOrWhiteSpace(prefix) && !prefix.EndsWith('.')) prefix = $"{prefix}.";
            foreach (var dependency in SameAccountDependencies ?? new string[] { })
                yield return new NuGetReference($"{prefix}{dependency}",
                    repositories.ContainsKey(dependency)
                        ? repositories[dependency].Version
                        : "1.0.0");
        }
    }
}
