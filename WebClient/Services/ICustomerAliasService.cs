using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;

namespace WebClient.Services
{
    public interface ICustomerAliasService
    {
        Task<int> CreateAsync(CreateCustomerAliasDto model);

        Task<PagingResult<CustomerAliasDto>> QueryAsync(CustomerAliasQueryDto query);

        Task<bool> DeleteByIdAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateCustomerAliasDto model);
    }
}
