﻿namespace ConsoleApp6
{
    public static class RepositoryInitializer
    {
        private const string origin = nameof(origin);

        public static void InitializeRepository(string repoUri, string repoDirectory)
        {
            LibGit2Sharp.Repository.Init(repoDirectory);
            using (var repo = new LibGit2Sharp.Repository(repoDirectory)) repo.Network.Remotes.Add(origin, repoUri);
        }
    }
}
