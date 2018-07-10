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
    public class CustomerAliasQueryRepository : ICustomerAliasQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public CustomerAliasQueryRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, PagingResult<CustomerAliases>> Query(CustomerAliasQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<CustomerAliases>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.CustomerAliases.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    var size = queryModel.Size <= QueryConstants.MaxElements ? queryModel.Size : QueryConstants.MaxElements;

                    var total = query.Count();
                    query = query
                        .Skip((queryModel.Page - 1) * size)
                        .Take(size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<CustomerAliases>>(results).ToList();
                    return new Right<Error, PagingResult<CustomerAliases>>(new PagingResult<CustomerAliases>(mapped, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<CustomerAliases>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
