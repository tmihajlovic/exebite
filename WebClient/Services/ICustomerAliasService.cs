using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;
using WebClient.Services.Core;

namespace WebClient.Services
{
    public interface ICustomerAliasService : IRestService<CreateCustomerAliasDto, CustomerAliasQueryDto, UpdateCustomerAliasDto, CustomerAliasDto>
    {
    }
}
