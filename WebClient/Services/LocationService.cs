using Exebite.DtoModels;
using WebClient.Services.Core;
using WebClient.Wrappers;

namespace WebClient.Services
{
    public class LocationService : RestService<CreateLocationDto, LocationQueryDto, UpdateLocationDto, LocationDto>, ILocationService
    {
        private const string _resourceUri = "location";

        public LocationService(IHttpClientWrapper client) : base(_resourceUri, client)
        {
        }
    }
}