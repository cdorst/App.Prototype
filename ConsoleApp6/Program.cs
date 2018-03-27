using Common.EntityFrameworkServices;
using ConsoleApp6.Templates;
using ConsoleApp6.Templates.CodeGenDeclarations;
using ConsoleApp6.Templates.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevOps.Build.AppVeyor.GetBuildRecord.BuildRecordGetter;
using static DevOps.Build.AppVeyor.GetRepositoryVersionRecord.RepositoryVersionRecordGetter;
using static System.Environment;

namespace ConsoleApp6
{
    public static class Program
    {
        private const string Comma = ",";
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static async Task Main(string[] args)
        {
            var path = args?.FirstOrDefault() ?? Path.Combine(CurrentDirectory, "declaration.json");

            // TODO: get from configuration
            var gitHubAccount = CDorstDevOpsDeclaration.CDorst;
            var project = gitHubAccount.Account;
            /*Write hard-coded repository declaration to JSON, then consume JSON below*/
            var repositoriesDeclaration = CDorstGitHubAccount.GetRepositories();
            var repositoriesJson = JsonConvert.SerializeObject(repositoriesDeclaration, _jsonSettings);
            // TODO: move JSON declaration into its own repo. Run this app on that repo's JSON during an AppVeyor "build". Ideally this app is a dotnet global tool. Until 2.1, .zip and deploy this tool to a CDN; cache downloaded tool on build image
            await File.WriteAllTextAsync(path, repositoriesJson);

            try
            {
                project = await GetAccountRepositoriesFromJson(path, project);
                var account = project.GetGitHubAccountDeclaration();
                var repositories = account.RepositoryList.GetRecords();
                RepositoriesFound(repositories.Count());
                var accountName = project.AccountName;

                foreach (var repository in repositories)
                {
                    var name = repository.GetName();
                    var version = repository.GetVersion();
                    var fullName = $"{accountName}.{name}";

                    var alreadyProcessed = (await GetBuildRecordAsync(fullName, version)) != null;
                    if (alreadyProcessed) continue;

                    var lastVersion = (await GetRepositoryVersionRecordAsync(accountName, name))?.Version;
                    var lastVersionInfo = string.IsNullOrWhiteSpace(lastVersion) ? null : await GetBuildRecordAsync(fullName, lastVersion);
                    var filesToCommit = GetFilesToCommit(repository, lastVersionInfo);
                    var dependenciesString = GetDependencyString(repositories, accountName, repository);
                    var hashString = GetHashString(repository);

                    WorkingOn(name);
                    await RepositoryCodeGenerator.CommitChanges(
                        account.AccountSettings.GitHubAccountSettings, repository, gitHubAccount.GitHubSecret, gitHubAccount.AppveyorSecret, filesToCommit, dependenciesString, hashString, accountName);
                    Done();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static HashSet<string> GetFilesToCommit(DevOps.Primitives.SourceGraph.Repository repository, DevOps.Build.AppVeyor.AzureStorageTableLedger.AppveyorBuildTable lastVersionInfo)
        {
            var hashSet = new HashSet<string>();
            if (lastVersionInfo != null)
                foreach (var hash in lastVersionInfo.FileHashes.Split(Comma).Distinct())
                    hashSet.Add(hash);

            var fileDict = new Dictionary<string, string>();
            foreach (var file in repository.RepositoryContent.RepositoryFileList.GetRecords())
                fileDict.TryAdd(file.GetPathRelativeToRepositoryRoot(), file.ComputeHash());

            var filesToCommit = new HashSet<string>();
            foreach (var file in fileDict)
                if (!hashSet.Contains(file.Value)) filesToCommit.Add(file.Key);

            return filesToCommit;
        }

        private static string GetDependencyString(IEnumerable<DevOps.Primitives.SourceGraph.Repository> repositories, string accountName, DevOps.Primitives.SourceGraph.Repository repository)
        {
            var dependencies = new StringBuilder();
            var deps = repository.RepositoryContent.SameAccountPackageDependencyList?.GetRecords().Select(r => r.Value).ToList();
            if (deps != null)
            {
                var subStrIdx = accountName.Length + 1;
                for (int i = 0; i < deps.Count; i++)
                {
                    var repo = deps[i];
                    dependencies.Append($"{repo}|{repositories.Where(r => r.GetName() == repo.Substring(subStrIdx)).First().RepositoryContent.Version.Value}");
                    if (i != deps.Count - 1) dependencies.Append(Comma);
                }
            }
            var dependenciesString = dependencies.ToString();
            return dependenciesString;
        }

        private static string GetHashString(DevOps.Primitives.SourceGraph.Repository repository)
        {
            var hashes = new StringBuilder();
            var files = repository.RepositoryContent.RepositoryFileList.GetRecords().ToList();
            for (int i = 0; i < files.Count; i++)
            {
                hashes.Append(files[i].ComputeHash());
                if (i != files.Count - 1) hashes.Append(Comma);
            }
            return hashes.ToString();
        }

        private static async Task<Declarations.Account> GetAccountRepositoriesFromJson(string path, Declarations.Account account)
        {
            var declaration = await GetCodeDeclarationFromJson(path);
            var dictionary = GetRepositoryDictionary(declaration);
            var accountName = account.AccountName;
            foreach (var repo in declaration ?? new List<ICodeGeneratable>())
            {
                if (repo is Class)
                {
                    account = account.WithCode((repo as Class).GetDeclaration().GetContent()(accountName, dictionary).Code);
                    continue;
                }
                if (repo is Interface)
                {
                    account = account.WithCode((repo as Interface).GetDeclaration().GetContent()(accountName, dictionary).Code);
                    continue;
                }
                if (repo is Metapackage)
                {
                    account = account.WithMetapackage((repo as Metapackage).GetDeclaration().GetContent()(accountName, dictionary).Metapackage);
                    continue;
                }
                if (repo is StaticFunction)
                {
                    account = account.WithCode((repo as StaticFunction).GetDeclaration().GetContent()(accountName, dictionary).Code);
                    continue;
                }
            }
            return account;
        }

        private static async Task<List<ICodeGeneratable>> GetCodeDeclarationFromJson(string path)
        {
            var json = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<ICodeGeneratable>>(json, _jsonSettings);
        }

        private static Dictionary<string, ITemplate> GetRepositoryDictionary(List<ICodeGeneratable> declaration)
            => new Dictionary<string, ITemplate>(
                declaration.Select(each => new KeyValuePair<string, ITemplate>(each.Name, each.GetDeclaration())));

        private static void Done()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done!");
        }

        private static void RepositoriesFound(int count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Repositories found in declaration: {count}");
        }

        private static void WorkingOn(string name)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Working on {name}...");
        }
    }
}
