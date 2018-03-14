using System.Collections.Generic;

namespace ConsoleApp6
{
    public class AppveyorAddProjectResponse
    {
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public List<object> Builds { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string RepositoryType { get; set; }
        public string RepositoryScm { get; set; }
        public string RepositoryName { get; set; }
        public bool IsPrivate { get; set; }
        public bool SkipBranchesWithoutAppveyorYml { get; set; }
        public string Created { get; set; }
    }
}
