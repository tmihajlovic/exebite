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
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly IMapper _mapper;
        private readonly IMealOrderingContextFactory _factory;

        public OrderQueryRepository(IMealOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Either<Error, PagingResult<Order>> GetAllOrdersForRestaurant(int restaruntId, int page, int size)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var restarurantMeals = context.Meal.SelectMany(x => x.FoodEntityMealEntities.Where(a => a.FoodEntity.RestaurantId == restaruntId)
                                                                                                 .Select(a => a.MealEntityId));

                    var query = context.Order.Where(o => restarurantMeals.Contains(o.MealId));
                    var total = query.Count();
                    var results = query.Skip((page - 1) * size)
                                       .Take(size);

                    var mapped = _mapper.Map<IList<Order>>(results).ToList();
                    return new Right<Error, PagingResult<Order>>(new PagingResult<Order>(mapped, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<Order>>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, PagingResult<Order>> Query(OrderQueryModel queryModel)
        {
            try
            {
                if (queryModel == null)
                {
                    return new Left<Error, PagingResult<Order>>(new ArgumentNotSet(nameof(queryModel)));
                }

                using (var context = _factory.Create())
                {
                    var query = context.Order.AsQueryable();

                    if (queryModel.Id != null)
                    {
                        query = query.Where(x => x.Id == queryModel.Id.Value);
                    }

                    if (queryModel.Date != null)
                    {
                        query = query.Where(x => x.Date == queryModel.Date.Value);
                    }

                    if (queryModel.CustomerId != null)
                    {
                        query = query.Where(x => x.CustomerId == queryModel.CustomerId.Value);
                    }

                    var total = query.Count();
                    query = query
                        .Skip((queryModel.Page - 1) * queryModel.Size)
                        .Take(queryModel.Size);

                    var results = query.ToList();
                    var mapped = _mapper.Map<IList<Order>>(results).ToList();
                    return new Right<Error, PagingResult<Order>>(new PagingResult<Order>(mapped, total));
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, PagingResult<Order>>(new UnknownError(ex.ToString()));
            }
        }
    }
}
