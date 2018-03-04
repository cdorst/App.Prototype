using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public static class AppveyorProjectAdder
    {
        public static async Task<AppveyorAddProjectResponse> AddApveyorProject(string appveyorToken, string accountName, string repositoryName)
        {
            var responseData = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appveyorToken);

                using (var response = await client.PostAsync("https://ci.appveyor.com/api/projects", new StringContent(JsonConvert.SerializeObject(new AppveyorNewProjectRequest(accountName, repositoryName, "gitHub")))))
                {
                    response.EnsureSuccessStatusCode();
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<AppveyorAddProjectResponse>(responseData);
        }
    }
}
