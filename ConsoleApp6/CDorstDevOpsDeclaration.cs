using ConsoleApp6.Declarations;
using static System.Environment;

namespace ConsoleApp6
{
    public static class CDorstDevOpsDeclaration
    {
        private const string APPVEYOR_API_TOKEN = nameof(APPVEYOR_API_TOKEN);
        private const string GITHUB_API_PERSONAL_ACCESS_TOKEN = nameof(GITHUB_API_PERSONAL_ACCESS_TOKEN);

        public static DevOpsDeclaration CDorst
            => new DevOpsDeclaration(
                GetEnvironmentVariable(APPVEYOR_API_TOKEN),
                GetEnvironmentVariable(GITHUB_API_PERSONAL_ACCESS_TOKEN),
                new CDorstAccount());
    }
}
