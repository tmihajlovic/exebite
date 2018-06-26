using System;
using AutoMapper;
using Either;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;


namespace Exebite.DataAccess.Repositories
{
    public class RestaurantCommandRepository : IRestaurantCommandRepository
    {
        private readonly IMapper _mapper;
        private readonly IFoodOrderingContextFactory _factory;

        public RestaurantCommandRepository(IMapper mapper, IFoodOrderingContextFactory factory)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public Either<Exception, int> Insert(RestourantInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var restaurantEntity = new RestaurantEntity()
                    {
                        Name = entity.Name,
                        DailyMenuId = entity.DailyMenuId,
                    };

                    var addedEntity = context.Restaurants.Add(restaurantEntity).Entity;
                    context.SaveChanges();
                    return new Right<Exception, int>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Exception, int>(ex);
            }
        }

        public Either<Exception, bool> Update(int id, RestourantUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Restaurants.Find(id);
                    currentEntity.DailyMenuId = entity.DailyMenuId;
                    currentEntity.Name = entity.Name;

                    context.SaveChanges();
                }

                return new Right<Exception, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<Exception, bool>(ex);
            }
        }

        public Either<Exception, bool> Delete(int id)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var itemSet = context.Set<RestaurantEntity>();
                    var item = itemSet.Find(id);
                    if (item != null)
                    {
                        itemSet.Remove(item);
                        context.SaveChanges();
                        return new Right<Exception, bool>(true);
                    }
                    else
                    {
                        return new Right<Exception, bool>(false);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Left<Exception, bool>(ex);
            }
        }
    }
}
