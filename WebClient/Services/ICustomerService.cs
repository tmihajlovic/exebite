using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;

namespace WebClient.Services
{
    public interface ICustomerService
    {
        Task<int> CreateAsync(CreateCustomerDto model);

        Task<PagingResult<CustomerDto>> QueryAsync(CustomerQueryDto query);

        Task DeleteByIdAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateCustomerDto model);
    }
}
