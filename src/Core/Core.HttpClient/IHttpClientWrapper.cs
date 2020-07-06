using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.HttpClient
{
    public interface IHttpClientWrapper
    {
        Task<TResponse> GetAsync<TResponse>(string resourceUrl, Dictionary<string, string> headers = null);
        Task<TResponse> PostAsync<TRequest, TResponse>(string resourceUrl, TRequest request, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> DeleteAsync(string resourceUrl, Dictionary<string, string> headers = null);
        Task<TResponse> PutAsync<TRequest, TResponse>(string resourceUrl, TRequest request, Dictionary<string, string> headers = null);
    }
}
