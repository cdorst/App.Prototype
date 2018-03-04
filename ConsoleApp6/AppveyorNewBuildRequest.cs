namespace ConsoleApp6
{
    public class AppveyorNewBuildRequest
    {
        public AppveyorNewBuildRequest() { }
        public AppveyorNewBuildRequest(string accountName, string projectSlug, string branchName)
        {
            AccountName = accountName;
            ProjectSlug = projectSlug;
            Branch = branchName;
        }
        public AppveyorNewBuildRequest(string accountName, string projectSlug)
            : this(accountName, projectSlug, "master")
        {
        }

        public string AccountName { get; set; }
        public string ProjectSlug { get; set; }
        public string Branch { get; set; }
    }
}
