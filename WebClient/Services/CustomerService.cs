using Exebite.DtoModels;
using WebClient.Services.Core;
using WebClient.Wrappers;

namespace WebClient.Services
{
    public class CustomerService : RestService<CreateCustomerDto, CustomerQueryDto, UpdateCustomerDto, CustomerDto>, ICustomerService
    {
        private const string _resourceUri = "Customer";

        public CustomerService(IHttpClientWrapper client) : base(_resourceUri, client)
        {
        }
    }
}
