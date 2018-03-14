using LibGit2Sharp;

namespace ConsoleApp6
{
    public static class BranchCanonicalNamer
    {
        public static string GetCanonicalName(Branch branch)
            => GetCanonicalName(branch.CanonicalName);

        private static string GetCanonicalName(string name)
            => $"{name}:{name}";
    }
}
