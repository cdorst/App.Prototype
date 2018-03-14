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
            FriendlyConsoleAppInitialization();
            try
            {
                var account = Input.Project.GetGitHubAccountDeclaration();
                var repositories = account.RepositoryList.GetRecords();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Repositories found in declaration: {repositories.Count()}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var repository in repositories)
                {
                    var name = repository.RepositoryNameDescription.Name.Value;
                    Console.WriteLine($"Working on {name}...");
                    await CommitChanges(
                        account.AccountSettings.GitHubAccountSettings, repository, Input.GitHubSecret, Input.AppveyorSecret);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Done!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void FriendlyConsoleAppInitialization()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("This app processes a DevOpsDeclaration.");
            Console.WriteLine();
            Console.WriteLine("Similar to declarative templating for cloud resouces, this project provides a normalized API that human or bot agents may use to setup GitHub repositories, AppVeyor->NuGet & AppVeyor->Azure build/release pipelines, author code and documentation, and Git commit & push changes to GitHub.");
            Console.WriteLine();
            Console.WriteLine("This app enables and encourages treating your GitHub account as a graph of single-responsibility repositories. For .NET Core NuGet packages, a single-responsibility is a single class (ideally with only a single line of code).");
            Console.WriteLine();
            Console.WriteLine("Use generics when possible and reuse/compose packages to minimize the number of repository-nodes that your implementation requires. Non-trivial declarations may require accounts with hundreds of packages or more. To manage this complexity, the app does the following:");
            Console.WriteLine("\t1) Code is published sequentially in dependency order");
            Console.WriteLine("\t2) NuGet artifacts are cached in blob-storage so that dependent builds are not time-bound by nuget.org's package upload/validate/index process");
            Console.WriteLine("\t3) A Package.Index repository is added to your GitHub account");
            Console.WriteLine("\t4) Package.Index's README lists all of the repositories in your graph along with shields.io DevOps pipeline badges");
            Console.WriteLine("\t4) Package.Index contains a graph.dgml file of your GitHub repositories as nodes and dependencies as links");
            Console.WriteLine();
            Console.WriteLine("NuGet deployments will fail if an identical version is already deployed.");
            Console.WriteLine("Azure deployments will only trigger if a dependent package version has updated.");
            Console.WriteLine("Submitting a declaration without implementing the version causes errors, but not destruction.");
            Console.WriteLine("If you currently have a working version deployed, (e.g. version 123.45.6), and apply an unwanted change (e.g. version 124.0.0), then copy the previously working declaration and re-apply as the highest version. In this hypothetical case, take 123.45.6, change the version number to 125.0.0 and then deploy.");
            Console.WriteLine("If you're deploying to a new account, the worst you can do is spend time deleting Azure resource groups, deleting GitHub repositories, deleting AppVeyor projects, and unlisting NuGet packages.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Close the window now if you want to cancel. Once working, do not close out of the window.");
            Console.WriteLine();
        }
    }
}
