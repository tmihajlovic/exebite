using Exebite.DtoModels;
using WebClient.Services.Core;

namespace WebClient.Services
{
    public interface ILocationService : IRestService<CreateLocationDto, LocationQueryDto, UpdateLocationDto, LocationDto>
    {
    }
}