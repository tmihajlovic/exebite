using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;

namespace WebClient.Services
{
    public interface ILocationService
    {
        Task<int> CreateAsync(CreateLocationDto model);

        Task<PagingResult<LocationDto>> QueryAsync(LocationQueryDto query);

        Task DeleteByIdAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateLocationDto model);
    }
}
