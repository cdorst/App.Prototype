using static System.IO.Path;

namespace ConsoleApp6
{
    public static class TempDirectoryGetter
    {
        public static string GetTempDirectory(string subdirectory = "repo")
            => Combine(GetTempPath(), subdirectory, GetRandomFileName());

    }
}
