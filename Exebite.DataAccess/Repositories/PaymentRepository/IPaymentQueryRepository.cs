using System.Collections.Generic;
using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IPaymentQueryRepository : IDatabaseQueryRepository<Payment, PaymentQueryModel>
    {
    }
}
