using System;
using System.Collections.Generic;
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

        public Either<Exception,(List<Restaurant>, int)> Query(RestaurantQueryModel queryModel)
        {
            try
            {
                queryModel = queryModel ?? new RestaurantQueryModel(1, 100);

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

                    var total = query.Count();

                    query = query
                        .Skip(queryModel.Page * queryModel.Size)
                        .Take(queryModel.Size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<Restaurant>>(results).ToList();
                    return new Right<Exception, (List<Restaurant>, int)>((mapped.ToList(), total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Exception, (List<Restaurant>, int)>(ex);
            }
        }
    }
}
