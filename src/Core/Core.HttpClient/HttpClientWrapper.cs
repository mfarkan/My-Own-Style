using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.HttpClient
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        public HttpClientWrapper(System.Net.Http.HttpClient client)
        {
            _httpClient = client;
        }
        private void AddHeadersToRequest(Dictionary<string, string> headers)
        {
            foreach (var item in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }
        public async Task<HttpResponseMessage> DeleteAsync(string resourceUrl, Dictionary<string, string> headers = null)
        {
            AddHeadersToRequest(headers);
            var result = await _httpClient.DeleteAsync(resourceUrl);
            return result;
        }

        public async Task<TResponse> GetAsync<TRequest, TResponse>(string resourceUrl, Dictionary<string, string> headers = null)
        {
            AddHeadersToRequest(headers);
            var result = await _httpClient.GetAsync(resourceUrl);
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(content);
            return response;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string resourceUrl, TRequest request, Dictionary<string, string> headers = null)
        {
            AddHeadersToRequest(headers);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(resourceUrl, content);
            var jsonResponse = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
            return response;
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string resourceUrl, TRequest request, Dictionary<string, string> headers = null)
        {
            AddHeadersToRequest(headers);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(resourceUrl, content);
            var jsonResponse = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
            return response;
        }
    }
}
