using System.IO;
using static System.IO.Path;

namespace ConsoleApp6
{
    public static class FileWriter
    {
        public static bool WriteFile(string repoDirectory, string fileContent, string relativeDirectory, string relativePath)
        {
            if (!string.IsNullOrEmpty(relativeDirectory))
                Directory.CreateDirectory(Combine(repoDirectory, relativeDirectory));
            var path = Combine(repoDirectory, relativePath);
            var exists = File.Exists(path);
            File.WriteAllText(path, fileContent);
            return exists;
        }
    }
}
