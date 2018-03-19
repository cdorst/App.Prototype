using ConsoleApp6.Declarations;
using static System.Environment;

namespace ConsoleApp6
{
    public static class CDorstDevOpsDeclaration
    {
        public static DevOpsDeclaration CDorst
            => new DevOpsDeclaration(
                GetEnvironmentVariable("APPVEYOR_API_TOKEN"),
                GetEnvironmentVariable("GITHUB_API_PERSONAL_ACCESS_TOKEN"),
                new CDorstProject());
    }
}
