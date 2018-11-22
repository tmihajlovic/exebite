using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DtoModels;
using WebClient.Services.Core;

namespace WebClient.Services
{
    public interface ICustomerService : IRestService<CreateCustomerDto, CustomerQueryDto, UpdateCustomerDto, CustomerDto>
    {
    }
}
