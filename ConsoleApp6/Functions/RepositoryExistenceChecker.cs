using DevOps.VersionControl.Structures.GitHub;
using System.Threading.Tasks;
using static DevOps.VersionControl.Functions.CheckIfGitHubRepositoryExists.GitHubRepositoryExistenceChecker;

namespace ConsoleApp6
{
    public static class RepositoryExistenceChecker
    {
        public static async Task<bool> RepositoryExists(string account, string repository)
            => await Exists(new AccountRepository(account, repository));
    }
}
