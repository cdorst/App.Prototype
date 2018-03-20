using Common.EntityFrameworkServices;
using ConsoleApp6.Templates;
using ConsoleApp6.Templates.CodeGenDeclarations;
using ConsoleApp6.Templates.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Environment;

namespace ConsoleApp6
{
    public static class Program
    {
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
            await File.WriteAllTextAsync(path, repositoriesJson);
            try
            {
                project = await GetAccountRepositoriesFromJson(path, project);
                var account = project.GetGitHubAccountDeclaration();
                var repositories = account.RepositoryList.GetRecords();
                RepositoriesFound(repositories.Count());
                foreach (var repository in repositories)
                {
                    var name = repository.RepositoryNameDescription.Name.Value;
                    WorkingOn(name);
                    await RepositoryCodeGenerator.CommitChanges(
                        account.AccountSettings.GitHubAccountSettings, repository, gitHubAccount.GitHubSecret, gitHubAccount.AppveyorSecret);
                    Done();
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
