using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public static class AppveyorProjectBuilder
    {
        public static async Task BuildApveyorProject(string appveyorToken, string accountName, string projectSlug)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appveyorToken);

                using (var response = await client.PostAsync("https://ci.appveyor.com/api/builds", new StringContent(JsonConvert.SerializeObject(new AppveyorNewBuildRequest(accountName, projectSlug, "gitHub")))))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}
