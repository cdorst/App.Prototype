using System.Threading.Tasks;
using static ConsoleApp6.AppveyorProjectAdder;
using static ConsoleApp6.AppveyorProjectBuilder;

namespace ConsoleApp6
{
    public static class AppveyorProjectAdderAndBuilder
    {
        public static async Task AddAndBuildAppveyorProject(string appveyorToken, string accountName, string repositoryName)
        {
            var response = await AddApveyorProject(appveyorToken, accountName, repositoryName);
            await BuildApveyorProject(appveyorToken, accountName, response.Slug);
        }
    }
}
