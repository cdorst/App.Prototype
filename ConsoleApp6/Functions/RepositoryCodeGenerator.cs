using DevOps.Primitives.SourceGraph;
using System.Linq;
using System.Threading.Tasks;
using static Common.Functions.ClearDirectory.DirectoryClearer;
using static ConsoleApp6.AppveyorBuildAdder;
using static ConsoleApp6.CommitMaker;
using static ConsoleApp6.GitHubRepositoryCreator;
using static ConsoleApp6.GitPusher;
using static ConsoleApp6.RepositoryCloneOrInitializer;
using static ConsoleApp6.RepositoryExistenceChecker;
using static ConsoleApp6.TempDirectoryGetter;

namespace ConsoleApp6
{
    public static class RepositoryCodeGenerator
    {
        public static async Task CommitChanges(GitHubAccountSettings account, Repository repository, string password, string appveyorToken)
        {
            var accountName = account.AccountName.Value;
            var repoNameDescription = repository.RepositoryNameDescription;
            var repositoryName = repoNameDescription.Name.Value;
            var repositoryDescription = repoNameDescription.Description.Value;
            var author = account.GitCommitSettings;
            var authorEmail = author.Email.Value;
            var authorName = author.Name.Value;

            var isNewRepo = await CloneOrCreateGitHubRepository(password, accountName, repositoryName, repositoryDescription);
            var repoDirectory = CloneOrInitGitRepository(accountName, repositoryName, isNewRepo);
            var anyChanges = CommitChanges(repository, authorEmail, authorName, repoDirectory);
            if (anyChanges) Push(repoDirectory, isNewRepo, accountName, password);
            await AddBuildForNewProjects(appveyorToken, accountName, repositoryName, isNewRepo);
        }

        private static async Task AddBuildForNewProjects(string appveyorToken, string accountName, string repositoryName, bool isNewRepo)
        {
            if (isNewRepo && repositoryName != "Project.Index")
                await AddAppveyorBuild(appveyorToken, accountName, repositoryName);
        }

        private static string CleanLocalDirectory()
        {
            var repoDirectory = GetTempDirectory();
            Clear(repoDirectory);
            return repoDirectory;
        }

        private static async Task<bool> CloneOrCreateGitHubRepository(string password, string accountName, string repositoryName, string repositoryDescription)
        {
            var isNewRepo = !(await RepositoryExists(accountName, repositoryName));
            if (isNewRepo) await CreateGitHubRepository(accountName, repositoryName, repositoryDescription, password);
            return isNewRepo;
        }

        private static string CloneOrInitGitRepository(string accountName, string repositoryName, bool isNewRepo)
        {
            var repoDirectory = CleanLocalDirectory();
            var repoUri = $"https://github.com/{accountName}/{repositoryName}.git";
            CloneOrInitializeRepository(repoDirectory, repoUri, isNewRepo);
            return repoDirectory;
        }

        private static bool CommitChanges(Repository repository, string authorEmail, string authorName, string repoDirectory)
        {
            var anyChanges = false;
            using (var repo = new LibGit2Sharp.Repository(repoDirectory))
                foreach (var file in repository.RepositoryContent.RepositoryFileList.GetAssociations().Select(f => f.GetRecord()))
                    anyChanges = TryMakeCommit(authorEmail, authorName, repoDirectory, anyChanges, repo, file);
            return anyChanges;
        }
    }
}
