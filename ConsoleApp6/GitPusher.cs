using LibGit2Sharp;
using static ConsoleApp6.BranchCanonicalNamer;

namespace ConsoleApp6
{
    public static class GitPusher
    {
        public static void Push(string repoDirectory, bool isNewRepo, string username, string password)
        {
            var credentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                (url, usernameFromUrl, types) => new UsernamePasswordCredentials { Username = username, Password = password });
            var pushOptions = new PushOptions { CredentialsProvider = credentialsProvider };
            using (var repo = new LibGit2Sharp.Repository(repoDirectory))
                if (isNewRepo)
                    repo.Network.Push(repo.Network.Remotes["origin"], GetCanonicalName(repo.Head), pushOptions);
                else
                    repo.Network.Push(repo.Head, pushOptions);
        }
    }
}
