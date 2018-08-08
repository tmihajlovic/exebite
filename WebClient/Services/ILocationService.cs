using Exebite.Common;
using Exebite.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface ILocationService
    {
        Task<int> CreateAsync(CreateLocationDto locationDto);

        Task<PagingResult<LocationDto>> QueryAsync(LocationQueryDto queryDto);

        Task DeleteByIdAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateLocationDto locationDto);
    }
}
