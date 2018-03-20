using LibGit2Sharp;
using static ConsoleApp6.BranchCanonicalNamer;

namespace ConsoleApp6
{
    public static class GitPusher
    {
        private const string origin = nameof(origin);

        public static void Push(string repoDirectory, bool isNewRepo, string username, string password)
        {
            using (var repo = new Repository(repoDirectory))
                if (isNewRepo)
                    repo.Network.Push(repo.Network.Remotes[origin], GetCanonicalName(repo.Head), GetOptions(username, password));
                else
                    repo.Network.Push(repo.Head, GetOptions(username, password));
        }

        private static LibGit2Sharp.Handlers.CredentialsHandler GetCredentialsHandler(string username, string password)
            => new LibGit2Sharp.Handlers.CredentialsHandler(
                (url, usernameFromUrl, types) => new UsernamePasswordCredentials { Username = username, Password = password });

        private static PushOptions GetOptions(LibGit2Sharp.Handlers.CredentialsHandler credentialsProvider)
            => new PushOptions { CredentialsProvider = credentialsProvider };

        private static PushOptions GetOptions(string username, string password)
            => GetOptions(GetCredentialsHandler(username, password));
    }
}
