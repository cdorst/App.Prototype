using Common.EntityFrameworkServices;
using DevOps.Primitives.SourceGraph;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Common.Functions.ClearDirectory.DirectoryClearer;
using static ConsoleApp6.AppveyorBuildAdder;
using static ConsoleApp6.CommitMaker;
using static ConsoleApp6.GitHubRepositoryCreator;
using static ConsoleApp6.GitPusher;
using static ConsoleApp6.RepositoryCloneOrInitializer;
using static ConsoleApp6.RepositoryExistenceChecker;
using static ConsoleApp6.TempDirectoryGetter;
using static DevOps.Build.AppVeyor.AddBuild.BuildAdder;
using static DevOps.Build.AppVeyor.AddRepositoryVersion.RepositoryVersionAdder;

namespace ConsoleApp6
{
    public static class RepositoryCodeGenerator
    {
        private const string ProjectIndex = "Project.Index";
        private const string QuoteChar = "\"";

        public static async Task CommitChanges(GitHubAccountSettings account, Repository repository, string password, string appveyorToken, HashSet<string> filesToCommit, string dependencies, string fileHashes, string namePrefix)
        {
            var accountName = account.AccountName.Value;
            var repoNameDescription = repository.RepositoryNameDescription;
            var repositoryName = repoNameDescription.Name.Value;
            var repositoryDescription = repoNameDescription.Description.Value;
            var author = account.GitCommitSettings;
            var authorEmail = author.Email.Value;
            var authorName = author.Name.Value;
            var version = repository.GetVersion();

            var isNewRepo = await CreateGitHubRepositoryIfNotExists(password, accountName, repositoryName, repositoryDescription.Replace(QuoteChar, string.Empty));
            var repoDirectory = CloneOrInitGitRepository(accountName, repositoryName, isNewRepo);
            var anyChanges = CommitChanges(repository, authorEmail, authorName, repoDirectory, filesToCommit);
            if (anyChanges) Push(repoDirectory, isNewRepo, accountName, password);
            await AddBuildAsync($"{namePrefix}.{repositoryName}", version, dependencies, fileHashes);
            await AddRepositoryVersionAsync(namePrefix, repositoryName, version);
            await AddBuildForNewProjects(appveyorToken, accountName, repositoryName, isNewRepo);
        }

        private static async Task AddBuildForNewProjects(string appveyorToken, string accountName, string repositoryName, bool isNewRepo)
        {
            if (isNewRepo && repositoryName != ProjectIndex)
                await AddAppveyorBuild(appveyorToken, accountName, repositoryName);
        }

        private static string CleanLocalDirectory()
        {
            var repoDirectory = GetTempDirectory();
            Clear(repoDirectory);
            return repoDirectory;
        }

        private static string CloneOrInitGitRepository(string accountName, string repositoryName, bool isNewRepo)
        {
            var repoDirectory = CleanLocalDirectory();
            var repoUri = $"https://github.com/{accountName}/{repositoryName}.git";
            CloneOrInitializeRepository(repoDirectory, repoUri, isNewRepo);
            return repoDirectory;
        }

        private static bool CommitChanges(Repository repository, string authorEmail, string authorName, string repoDirectory, HashSet<string> filesToCommit)
        {
            var allFiles = repository.RepositoryContent.RepositoryFileList.GetRecords();
            var commitFiles = new List<RepositoryFile>();
            foreach (var file in allFiles) if (filesToCommit.Contains(file.GetPathRelativeToRepositoryRoot())) commitFiles.Add(file);
            var anyChanges = false;
            using (var repo = new LibGit2Sharp.Repository(repoDirectory))
                foreach (var file in commitFiles)
                    anyChanges = TryMakeCommit(authorEmail, authorName, repoDirectory, anyChanges, repo, file);
            return anyChanges;
        }

        private static async Task<bool> CreateGitHubRepositoryIfNotExists(string password, string accountName, string repositoryName, string repositoryDescription)
        {
            var isNewRepo = !(await RepositoryExists(accountName, repositoryName));
            if (isNewRepo) await CreateGitHubRepository(accountName, repositoryName, repositoryDescription, password);
            return isNewRepo;
        }
    }
}
