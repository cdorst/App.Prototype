using ConsoleApp6.Declarations;

namespace ConsoleApp6
{
    public class DevOpsDeclaration
    {
        public DevOpsDeclaration() { }
        public DevOpsDeclaration(string appveyorSecret, string gitHubSecret, Project project)
        {
            AppveyorSecret = appveyorSecret;
            GitHubSecret = gitHubSecret;
            Project = project;
        }

        public string AppveyorSecret { get; set; }
        public string GitHubSecret { get; set; }
        public Project Project { get; set; }
    }
}
