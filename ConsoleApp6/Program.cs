using Common.EntityFrameworkServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ConsoleApp6.RepositoryCodeGenerator;

namespace ConsoleApp6
{
    public static class Program
    {
        public static DevOpsDeclaration Input = CDorstDevOpsDeclaration.CDorst;

        public static async Task Main(string[] args)
        {
            try
            {
                var account = Input.Project.GetGitHubAccountDeclaration();
                var repositories = account.RepositoryList.GetRecords();
                RepositoriesFound(repositories.Count());
                foreach (var repository in repositories)
                {
                    var name = repository.RepositoryNameDescription.Name.Value;
                    WorkingOn(name);
                    await CommitChanges(
                        account.AccountSettings.GitHubAccountSettings, repository, Input.GitHubSecret, Input.AppveyorSecret);
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
