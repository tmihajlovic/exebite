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
    public class DailyMenuQueryRepository : IDailyMenuQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public DailyMenuQueryRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, PagingResult<DailyMenu>> Query(DailyMenuQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<DailyMenu>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.DailyMenu.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    if (queryModel.RestaurantId != null)
                    {
                        query = query.Where(x => x.RestaurantId == queryModel.RestaurantId.Value);
                    }

                    var size = queryModel.Size <= QueryConstants.MaxElements ? queryModel.Size : QueryConstants.MaxElements;

                    var total = query.Count();
                    query = query
                        .Skip((queryModel.Page - 1) * size)
                        .Take(size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<DailyMenu>>(results).ToList();
                    return new Right<Error, PagingResult<DailyMenu>>(new PagingResult<DailyMenu>(mapped, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<DailyMenu>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
