using static ConsoleApp6.RepositoryInitializer;

namespace ConsoleApp6
{
    public static class RepositoryCloneOrInitializer
    {
        public static void CloneOrInitializeRepository(string repoDirectory, string repoUri, bool isNewRepo)
        {
            if (isNewRepo)
                InitializeRepository(repoUri, repoDirectory);
            else
                LibGit2Sharp.Repository.Clone(repoUri, repoDirectory);
        }
    }
}
