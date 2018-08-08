using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebClient.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public HttpClientWrapper(IConfiguration configuration)
        {
            _configuration = configuration;

            var uri = _configuration.GetSection("Api")["Url"];
            _client = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await _client.DeleteAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value)
        {
            return await _client.PostAsJsonAsync(requestUri, value);
        }

        public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
        {
            return await _client.PutAsJsonAsync(requestUri, value);
        }
    }
}