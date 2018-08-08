using System;
using System.Net.Http;
using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;
using WebClient.Extensions;

namespace WebClient.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _client;

        private const string _baseUrl = "http://localhost:29796/api/";

        public LocationService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<int> CreateAsync(CreateLocationDto model)
        {
            var response = await _client.PostAsJsonAsync("location", model).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
            return body.id;
        }

        public async Task<PagingResult<LocationDto>> QueryAsync(LocationQueryDto query)
        {
            var url = "location/Query?" + query.BuildQuery();

            var response = await _client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<PagingResult<LocationDto>>().ConfigureAwait(false);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var response = await _client.DeleteAsync("location/" + id).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> UpdateAsync(int id, UpdateLocationDto model)
        {
            var response = await _client.PutAsJsonAsync("location/" + id, model);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsAsync<dynamic>().ConfigureAwait(false);
            return body.updated;
        }
    }
}