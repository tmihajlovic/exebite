using Exebite.Common;
using Exebite.DtoModels;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebClient.Extensions;

namespace WebClient.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _client;

        private const string _baseUrl = "http://localhost:29796/api/";

        public LocationService()
        {
            _client = new HttpClient();
        }

        public async Task<int> CreateAsync(CreateLocationDto locationDto)
        {
            var response = await _client.PostAsJsonAsync(_baseUrl + "location", locationDto);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsAsync<dynamic>();
            return body.id;
        }

        public async Task<PagingResult<LocationDto>> QueryAsync(LocationQueryDto queryDto)
        {
            var url = _baseUrl + "location/Query?" + queryDto.BuildQuery();

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<PagingResult<LocationDto>>();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var response = await _client.DeleteAsync(_baseUrl + "location/" + id);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> UpdateAsync(int id, UpdateLocationDto locationDto)
        {
            var response = await _client.PutAsJsonAsync(_baseUrl + "location/" + id, locationDto);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsAsync<dynamic>();
            return body.updated;
        }


    }
}