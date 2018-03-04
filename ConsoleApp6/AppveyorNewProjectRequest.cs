namespace ConsoleApp6
{
    public class AppveyorNewProjectRequest
    {
        public AppveyorNewProjectRequest(string repositoryName, string repositoryProvider)
        {
            RepositoryName = RepositoryName;
            RepositoryProvider = RepositoryProvider;
        }
        public AppveyorNewProjectRequest(string account, string repository, string provider)
            : this($"{account}/{repository}", provider)
        { }

        public string RepositoryName { get; set; }
        public string RepositoryProvider { get; set; }
    }
}
