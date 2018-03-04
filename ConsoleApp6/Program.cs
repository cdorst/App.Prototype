using DevOps.Primitives.SourceGraph;
using System.Linq;
using System.Threading.Tasks;
using static Common.Functions.ClearDirectory.DirectoryClearer;
using static ConsoleApp6.AppveyorProjectAdderAndBuilder;
using static ConsoleApp6.CommitMaker;
using static ConsoleApp6.GitHubRepositoryCreator;
using static ConsoleApp6.GitPusher;
using static ConsoleApp6.RepositoryCloneOrInitializer;
using static ConsoleApp6.RepositoryExistenceChecker;
using static ConsoleApp6.TempDirectoryGetter;
using static System.Environment;

namespace ConsoleApp6
{
    /// <summary>
    /// Prototype for cloud code-generation & CI,CD as a service; declarative DevOps; DevOps as data;
    /// </summary>
    public static class Program
    {
        // Get GitHubAccount graph (ex: incoming PUT request from web client)
        // Save graph (ex: PUT API invoking EF upsert)
        // Get previousVersion.RepositoryListId
        // if saved.RepositoryListId != previousVersion.RepositoryListId
        //


        public static async Task Main(string[] args)
        {
            // TODO: Deploy program as durable function triggered by EventGrid event (fired from AspNetCore as ApplicationTopic)
            // Event.data is changeSet.Id

            // TODO: derive ChangeSets from declaration objects
            // ChangeSets are units of work. Declarations are full representations of repositories
            // Scenario:
            //      user-agent edits declaration (humans using PWA, or bots using API)
            //      API emits 'declaration-changed' EventGrid.ApplicationTopic
            //      durable func handles event
            //          var ids = await durable.activity.GetRepositoryStateGraphIDs(event.data.RepositoryID);
            //          var current = await durable.activity.GetRepositoryGraph(ids.Current);
            //          var previous = await durable.activity.GetRepositoryGraph(ids.Previous);
            //          var changeSet = await durable.activity.GenerateChangeSet(current, previous); // No StringReferences loaded, only keys
            //          await durable.activity.SaveChangeSetToBlobStorage(changeSet);
            //          await durable.activity.EmitEventGridApplicationTopicEvent(changeSet.Id);

            // TODO: Make this work for any GitHub account. Use variable for user/pass
            // Lookup password from secure store using changeSet.Repository.Account
            // Use Azure Key Vault GetSecret/SetSecret api? SetSecret in web app; GetSecret in durable function activity
            var githubSecret = GetEnvironmentVariable("GITHUB_API_PERSONAL_ACCESS_TOKEN");
            var appveyorSecret = GetEnvironmentVariable("APPVEYOR_API_TOKEN");

            var account = default(GitHubAccount);
            if (account != null)
            {
                var repositories = account.RepositoryList?
                    .GetAssociations().Select(r => r.Repository);
                foreach (var repository in repositories)
                {
                    // TODO: invoke with no strings loaded in changeSet.
                    // Load each string inside commit loop.
                    //      local fs -> cdn -> API -> http HEAD cdn -> redirect to cdn on hit -> redis on cdn miss (once retrieved, add to blob storage/cdn) -> sql on redis miss
                    await ApplyChanges(
                        account.GitHubAccountSettings, repository, githubSecret, appveyorSecret);
                }
            }
        }

        private static async Task ApplyChanges(GitHubAccountSettings account, Repository repository, string password, string appveyorToken)
        {
            var accountName = account.AccountName.Value;
            var repoNameDescription = repository.RepositoryNameDescription;
            var repositoryName = repoNameDescription.Name.Value;
            var repositoryDescription = repoNameDescription.Description.Value;
            var author = account.GitCommitSettings;
            var authorEmail = author.Email.Value;
            var authorName = author.Name.Value;

            // Prepare local directory
            var repoDirectory = GetTempDirectory();
            Clear(repoDirectory);

            // Create repository on GitHub (if not exists)
            var isNewRepo = !(await RepositoryExists(accountName, repositoryName));
            if (isNewRepo) await CreateGitHubRepository(accountName, repositoryName, repositoryDescription, password);

            // Clone or init local git repository
            var repoUri = $"https://github.com/{accountName}/{repositoryName}.git";
            CloneOrInitializeRepository(repoDirectory, repoUri, isNewRepo);

            // Make commit
            var anyChanges = false;
            using (var repo = new LibGit2Sharp.Repository(repoDirectory))
                foreach (var file in repository.RepositoryFileList.GetAssociations().Select(f => f.GetRecord()))
                    anyChanges = TryMakeCommit(authorEmail, authorName, repoDirectory, anyChanges, repo, file);

            // Push changes
            if (anyChanges) Push(repoDirectory, isNewRepo, accountName, password);

            // Add and queue CI/CD pipeline
            if (isNewRepo) await AddAndBuildAppveyorProject(appveyorToken, accountName, repositoryName);
        }
    }
}
