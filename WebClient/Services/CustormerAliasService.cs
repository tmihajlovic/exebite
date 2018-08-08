using Exebite.DtoModels;
using WebClient.Services.Core;
using WebClient.Wrappers;

namespace WebClient.Services
{
    public class CustormerAliasService : RestService<CreateCustomerAliasDto, CustomerAliasQueryDto, UpdateCustomerAliasDto, CustomerAliasDto>, ICustomerAliasService
    {
        private const string _resourceUri = "CustomerAliases";

        public CustormerAliasService(IHttpClientWrapper client) : base(_resourceUri, client)
        {
        }
    }
}