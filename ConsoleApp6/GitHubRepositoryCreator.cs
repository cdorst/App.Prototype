using Common.Structures.HttpBasicAuthentication;
using System.Threading.Tasks;
using static DevOps.VersionControl.Functions.CreateGitHubRepository.GitHubRepositoryCreator;

namespace ConsoleApp6
{
    public static class GitHubRepositoryCreator
    {
        public static async Task CreateGitHubRepository(string account, string name, string description, string password)
            => await Create(name, description, new BasicAuthenticationCredentials(account, password));
    }
}
