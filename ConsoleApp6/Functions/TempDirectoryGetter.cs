using static System.IO.Path;

namespace ConsoleApp6
{
    public static class TempDirectoryGetter
    {
        private const string repo = nameof(repo);

        public static string GetTempDirectory(string subdirectory = repo)
            => Combine(GetTempPath(), subdirectory, GetRandomFileName());
    }
}
