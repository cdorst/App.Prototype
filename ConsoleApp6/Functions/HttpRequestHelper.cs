using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal static class HttpRequestHelper
    {
        private const string Bearer = nameof(Bearer);
        private const string ContentType = "application/json";

        public static async Task<string> PostJson(string uri, string token, string json, bool skipResponse = false)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = GetAuthenticationHeader(token);
                using (var response = await client.PostAsync(uri, GetContent(json)))
                {
                    response.EnsureSuccessStatusCode();
                    return skipResponse
                        ? await Task.FromResult<string>(null)
                        : await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static AuthenticationHeaderValue GetAuthenticationHeader(string token)
            => new AuthenticationHeaderValue(Bearer, token);

        private static StringContent GetContent(string json)
            => new StringContent(json, Encoding.UTF8, ContentType);
    }
}
