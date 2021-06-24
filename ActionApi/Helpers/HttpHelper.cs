using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace ActionApi.Helpers
{
    public static class HttpHelper
    {
        public static async Task<T> PostAsync<T>(object payload, Uri address)
        {
            var client = new RestClient(address.AbsoluteUri);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(payload);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static async Task<string> GetAsync(Uri address)
        {
            var client = new RestClient(address.AbsoluteUri);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);

            return response.Content;
        }
    }
}
