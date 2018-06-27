using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.DataAccess.Context;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class RestaurantQueryRepository : IRestaurantQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public RestaurantQueryRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }


        public Either<Error, PagingResult<Restaurant>> Query(RestaurantQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    throw new ArgumentNullException(nameof(queryModel));
                }

                using (var context = _factory.Create())
                {
                    var query = context.Restaurants.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(queryModel.Name))
                    {
                        query = query.Where(x => x.Name == queryModel.Name);
                    }

                    query = query
                        .Skip((queryModel.Page - 1) * queryModel.Size)
                        .Take(queryModel.Size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<Restaurant>>(results).ToList();
                    return new Right<Error, PagingResult<Restaurant>>(new PagingResult<Restaurant>(mapped, query.Count()));
                }
            }
            catch (ArgumentNullException ex)
            {
                return new Left<Error, PagingResult<Restaurant>>(new ArgumentNotSet(ex.ToString()));
            }
            catch (Exception ex)
            {

                return new Left<Error, PagingResult<Restaurant>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
