using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Wrappers
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T value);

        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> DeleteAsync(string requestUri);

        Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value);
    }
}