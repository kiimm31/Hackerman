using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ActionApi.Helpers
{
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> PostAsync<T>(object payload, Uri address)
        {
            var response = await _httpClient.PostAsJsonAsync(address, payload);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<string> GetAsync(Uri address)
        {
            var response = await _httpClient.GetAsync(address);

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
