using Common.EntityFrameworkServices;
using DevOps.Primitives.NuGet;
using DevOps.Primitives.SourceGraph;
using DevOps.Primitives.SourceGraph.Helpers.Consolidated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Common.Functions.CheckNullableEnumerationForAnyElements.NullableEnumerationAny;
using static DevOps.Primitives.SourceGraph.Helpers.Common.Files.ReamdmeFileHelper;
using static DevOps.Primitives.SourceGraph.Helpers.ProjectIndex.Repositories.ProjectIndexRepositories;

namespace ConsoleApp6.Declarations
{
    internal static class ProjectIndexRepositoryAdder
    {
        private static readonly string[] _empty = new string[] { };
        private static readonly KeyValuePair<string, IEnumerable<string>>[] _emptyPairs = new KeyValuePair<string, IEnumerable<string>>[] { };

        public static GitHubAccount AddProjectIndexRepository(Account account)
        {
            var prefix = $"{account.AccountName}.";
            var dictionary = BuildDependencyDictionary(account, prefix);
            // Get declaration
            var declaration = account.GenerateAccount();
            // Overwrite READMEs to add "Dependents" section
            var modified = ChangeReadmeFiles(prefix, dictionary, declaration);
            // Add Project.Index repository
            modified.Add(ProjectIndexRepository(dictionary));
            // Package & ship declaration object
            declaration.RepositoryList = new RepositoryList(modified.Select(r => new RepositoryListAssociation(r)).ToList());
            return declaration;
        }

        private static Dictionary<string, IEnumerable<string>> BuildDependencyDictionary(Account account, string prefix)
        {
            Func<string, bool> filter = name => name.StartsWith(prefix);
            Func<NuGetReference, string> selector = package => package.Include.Value;
            Func<string, List<NuGetReference>, KeyValuePair<string, IEnumerable<string>>> genericSelector = (name, dependencies)
                => new KeyValuePair<string, IEnumerable<string>>($"{prefix}{name}", dependencies?.Select(selector).Where(filter) ?? _empty);
            Func<Code, KeyValuePair<string, IEnumerable<string>>> codeSelector = repo => genericSelector(repo.ProjectName, repo.Dependencies);
            Func<Metapackage, KeyValuePair<string, IEnumerable<string>>> metapackageSelector = repo => genericSelector(repo.ProjectName, repo.Dependencies);
            var code = account.Code?.Select(codeSelector) ?? _emptyPairs;
            var metapackages = account.Metapackages?.Select(metapackageSelector) ?? _emptyPairs;
            return new Dictionary<string, IEnumerable<string>>(code.Concat(metapackages));
        }

        private static List<Repository> ChangeReadmeFiles(string prefix, Dictionary<string, IEnumerable<string>> dictionary, GitHubAccount declaration)
        {
            var repositories = new List<Repository>();
            foreach (var repository in declaration.RepositoryList.GetRecords())
            {
                var repositoryName = repository.GetName();
                var repositoryKey = $"{prefix}{repositoryName}";
                if (dictionary.ContainsKey(repositoryKey))
                {
                    var content = repository.RepositoryContent;
                    var dependents = dictionary[repositoryKey];
                    if (Any(dependents))
                    {
                        var fileList = new List<RepositoryFile>();
                        var files = content.RepositoryFileList.GetRecords();
                        foreach (var file in files) fileList.Add(EditReadmeFile(prefix, dependents, file));
                        content.RepositoryFileList = new RepositoryFileList(fileList.ToArray());
                    }
                    repository.RepositoryContent = content;
                }
                repositories.Add(repository);
            }
            return repositories;
        }

        private static RepositoryFile EditReadmeFile(string prefix, IEnumerable<string> dependents, RepositoryFile file)
        {
            var fileName = file.FileName;
            if (fileName.Name.Value == ReadmeFileName && !(fileName.PathParts?.GetAssociations()?.Any() ?? false)) // root README.md file
            {
                var fileContent = file.Content;
                var many = dependents.Count() > 1;
                var projectProjects = many ? "projects" : "project";
                var useUses = many ? "use" : "uses";
                var injectContent = new StringBuilder()
                    .AppendLine("## Dependents").AppendLine()
                    .AppendLine($"The {projectProjects} below {useUses} this repository as a direct dependency.").AppendLine()
                    .AppendLine("Name | Status")
                    .AppendLine("---- | ------");
                foreach (var dependent in dependents)
                {
                    var dependentRepoName = dependent.Substring(prefix.Length);
                    injectContent.AppendLine($"[{dependentRepoName}](https://github.com/{prefix}/{dependentRepoName}) | {GetBadges(prefix, dependentRepoName)}");
                }
                injectContent.AppendLine();
                fileContent.Value = fileContent.Value.Replace("## NuGet", injectContent.AppendLine("## NuGet").ToString());
                file.Content = fileContent;
            }
            return file;
        }
    }
}
