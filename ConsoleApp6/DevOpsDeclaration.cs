using ConsoleApp6.Declarations;

namespace ConsoleApp6
{
    public class DevOpsDeclaration
    {
        public DevOpsDeclaration() { }
        public DevOpsDeclaration(string appveyorSecret, string gitHubSecret, Account account)
        {
            Account = account;
            AppveyorSecret = appveyorSecret;
            GitHubSecret = gitHubSecret;
        }

        public Account Account { get; set; }
        public string AppveyorSecret { get; set; }
        public string GitHubSecret { get; set; }
    }
}
