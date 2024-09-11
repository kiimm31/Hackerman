using System.Threading.Tasks;
using System;

namespace ActionApi.Helpers
{
    public interface IHttpHelper
    {
        Task<T> PostAsync<T>(object payload, Uri address);
        Task<string> GetAsync(Uri address);
    }
}