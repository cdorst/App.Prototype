using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal static class HttpRequestHelper
    {
        public static async Task<string> PostJson(string uri, string token, string json, bool skipResponse = false)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json")))
                {
                    response.EnsureSuccessStatusCode();
                    return skipResponse
                        ? await Task.FromResult<string>(null)
                        : await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
