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
    public class FoodQueryRepository : IFoodQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IMealOrderingContextFactory _factory;

        public FoodQueryRepository(IMealOrderingContextFactory factory, IMapper mapper)
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
                    var query = context.Food.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    if (queryModel.RestaurantId != null)
                    {
                        query = query.Where(x => x.RestaurantId == queryModel.RestaurantId.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(queryModel.Name))
                    {
                        query = query.Where(x => x.Name == queryModel.Name);
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

        public Either<Error, int> FindByNameAndRestaurantId(FoodQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, int>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var ctx = _factory.Create())
                {
                    var record = ctx.Food.FirstOrDefault(c =>
                        c.Name.Equals(queryModel.Name, StringComparison.OrdinalIgnoreCase) &&
                        c.RestaurantId == queryModel.RestaurantId);
                    return new Right<Error, int>(record?.Id ?? 0);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, int>(new UnknownError(ex.ToString()));
            }
        }
    }
}
