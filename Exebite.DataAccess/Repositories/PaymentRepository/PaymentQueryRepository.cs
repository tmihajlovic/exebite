using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class PaymentQueryRepository : IPaymentQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public PaymentQueryRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, PagingResult<Payment>> Query(PaymentQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<Payment>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.Payment.AsQueryable();

                    if (queryModel.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == queryModel.Id);
                    }

                    var total = query.Count();

                    query = query
                        .Skip((queryModel.Page - 1) * queryModel.Size)
                        .Take(queryModel.Size);

                    var recepieEntities = query.ToList();
                    var recepies = _mapper.Map<IList<Payment>>(recepieEntities).ToList();
                    return new Right<Error, PagingResult<Payment>>(new PagingResult<Payment>(recepies, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<Payment>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
