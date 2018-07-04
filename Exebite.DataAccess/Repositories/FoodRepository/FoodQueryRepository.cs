using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Either;
using Exebite.DataAccess.Context;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class FoodQueryRepository : IFoodQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public FoodQueryRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, PagingResult<Food>> Query(FoodQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<Food>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.Foods.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    if (queryModel.RestaurantId != null)
                    {
                        query = query.Where(x => x.RestaurantId == queryModel.RestaurantId.Value);
                    }

                    var total = query.Count();
                    query = query
                        .Skip((queryModel.Page - 1) * queryModel.Size)
                        .Take(queryModel.Size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<Food>>(results).ToList();
                    return new Right<Error, PagingResult<Food>>(new PagingResult<Food>(mapped, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<Food>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
