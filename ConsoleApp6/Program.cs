using Common.EntityFrameworkServices;
using ConsoleApp6.Templates;
using ConsoleApp6.Templates.CodeGenDeclarations;
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
        public static async Task Main(string[] args)
        {
            try
            {

                var path = args?.FirstOrDefault() ?? Path.Combine(CurrentDirectory, "declaration.json");

                var gitHubAccount = CDorstDevOpsDeclaration.CDorst;
                var project = gitHubAccount.Project;
                path = Path.Combine(CurrentDirectory, "cdorst-declaration.json");

                //var declaration = CDorstGitHubAccount.GetRepositories();
                //var json = JsonConvert.SerializeObject(declaration, jsonSettings);
                //await File.WriteAllTextAsync(path, json);

                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto
                };
                var json = await File.ReadAllTextAsync(path);
                var declaration = JsonConvert.DeserializeObject<List<ICodeGeneratable>>(json, jsonSettings);
                var repos = declaration.Select(each => new KeyValuePair<string, ITemplate>(each.Name, each.GetDeclaration()));
                var dictionary = new Dictionary<string, ITemplate>(repos);
                foreach (var repo in declaration ?? new List<ICodeGeneratable>())
                {
                    if (repo is Class)
                    {
                        var @class = repo as Class;
                        project.WithCode(@class.GetDeclaration().GetContent()(dictionary).Code);
                    }
                    if (repo is Interface)
                    {
                        var @interface = repo as Interface;
                        project.WithCode(@interface.GetDeclaration().GetContent()(dictionary).Code);
                    }
                    if (repo is Metapackage)
                    {
                        var package = repo as Metapackage;
                        project.WithMetapackage(package.GetDeclaration().GetContent()(dictionary).Metapackage);
                    }
                    if (repo is StaticFunction)
                    {
                        var function = repo as StaticFunction;
                        project.WithCode(function.GetDeclaration().GetContent()(dictionary).Code);
                    }
                }

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
