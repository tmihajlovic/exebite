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

        public RestaurantCommandRepository(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _mapper = mapper;
            _factory = factory;
        }

        public Either<Error, int> Insert(RestaurantInsertModel entity)
        {
            try
            {
                using (var context = _factory.Create())
                {
                    var restaurantEntity = new RestaurantEntity()
                    {
                        Name = entity.Name,
                    };

                    var addedEntity = context.Restaurants.Add(restaurantEntity).Entity;
                    context.SaveChanges();
                    return new Right<Error, int>(addedEntity.Id);
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, int>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Update(int id, RestaurantUpdateModel entity)
        {
            try
            {
                if (entity == null)
                {
                    return new Left<Error, bool>(new ArgumentNotSet(nameof(entity)));
                }

                using (var context = _factory.Create())
                {
                    var currentEntity = context.Restaurants.Find(id);
                    if (currentEntity == null)
                    {
                        return new Left<Error, bool>(new RecordNotFound(nameof(entity)));
                    }

                    currentEntity.Name = entity.Name;
                    context.SaveChanges();
                }

                return new Right<Error, bool>(true);
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }

        public Either<Error, bool> Delete(int id)
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
                        return new Right<Error, bool>(true);
                    }
                    else
                    {
                        return new Right<Error, bool>(false);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Left<Error, bool>(new UnknownError(ex.ToString()));
            }
        }
    }
}
