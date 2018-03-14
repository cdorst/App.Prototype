using Newtonsoft.Json;
using System.Threading.Tasks;
using static ConsoleApp6.HttpRequestHelper;

namespace ConsoleApp6
{
    internal static class AppveyorBuildAdder
    {
        public static async Task AddAppveyorBuild(string token, string account, string repository)
        {
            account = account.ToLower();
            var response = await PostJson(GetAppveyorUri("projects"), token, GetAppveyorAddBuildJson(account, repository));
            await PostJson(GetAppveyorUri("builds"), token, GetAppveyorQueueBuildJson(account, response), true);
        }

        private static string GetAppveyorAddBuildJson(string account, string repository)
            => $"{{\"repositoryProvider\":\"gitHub\",\"repositoryName\":\"{account}/{repository}\"}}";

        private static string GetAppveyorQueueBuildJson(string account, string response)
            => $"{{\"accountName\":\"{account}\",\"projectSlug\":\"{GetSlug(response)}\",\"branch\":\"master\"}}";

        private static string GetAppveyorUri(string resource)
            => $"https://ci.appveyor.com/api/{resource}";

        private static string GetSlug(string addProjectResponse)
            => JsonConvert.DeserializeObject<AppveyorAddProjectResponse>(addProjectResponse).Slug;
    }
}
