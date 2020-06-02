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
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IMealOrderingContextFactory _factory;

        public CustomerQueryRepository(IMealOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, bool> ExistsByGoogleId(string googleId)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    if (string.IsNullOrWhiteSpace(googleId))
                    {
                        return new Left<Error, bool>(new ArgumentNotSet(nameof(googleId)));
                    }

                    using (var ctx = _factory.Create())
                    {
                        var exists = ctx.Customer.FirstOrDefault(c => c.GoogleUserId.Equals(googleId)) != null;
                        return new Right<Error, bool>(exists);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, string> GetRole(string googleId)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var customer = context.Customer.FirstOrDefault(x => x.GoogleUserId == googleId);
                    if (customer == null)
                    {
                        return new Left<Error, string>(new RecordNotFound($"Record with GoogleUserId='{googleId}' is not found."));
                    }

                    return new Right<Error, string>(Enum.GetName(typeof(RoleType), customer.Role));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, string>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, PagingResult<Customer>> Query(CustomerQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<Customer>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.Customer.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(queryModel.GoogleUserId))
                    {
                        query = query.Where(x => x.GoogleUserId == queryModel.GoogleUserId);
                    }

                    if (queryModel.IsActive != null)
                    {
                        query = query.Where(x => x.IsActive == queryModel.IsActive);
                    }

                    var size = queryModel.Size <= QueryConstants.MaxElements ? queryModel.Size : QueryConstants.MaxElements;

                    var total = query.Count();
                    query = query
                        .Skip((queryModel.Page - 1) * size)
                        .Take(size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<Customer>>(results).ToList();
                    return new Right<Error, PagingResult<Customer>>(new PagingResult<Customer>(mapped, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<Customer>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
