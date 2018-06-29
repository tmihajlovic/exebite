using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public class RestaurantRepository : DatabaseRepository<Restaurant, RestaurantEntity, RestaurantQueryModel>, IRestaurantRepository
    {
        public RestaurantRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<IRestaurantRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public Restaurant GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("Name can't be empty");
            }

            using (var context = _factory.Create())
            {
                var restaurantEntity = context.Restaurants.Where(r => r.Name == name).FirstOrDefault();
                if (restaurantEntity == null)
                {
                    return null;
                }

                return _mapper.Map<Restaurant>(restaurantEntity);
            }
        }

        public override Restaurant Insert(Restaurant entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var restaurantEntity = _mapper.Map<RestaurantEntity>(entity);

                var addedEntity = context.Restaurants.Add(restaurantEntity).Entity;
                context.SaveChanges();
                return _mapper.Map<Restaurant>(addedEntity);
            }
        }

        public override Restaurant Update(Restaurant entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var currentEntity = context.Restaurants.Find(entity.Id);
                currentEntity.Name = entity.Name;

                context.SaveChanges();

                var resultEntity = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                return _mapper.Map<Restaurant>(resultEntity);
            }
        }

        public override IList<Restaurant> Query(RestaurantQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
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

                var results = query.ToList();
                return _mapper.Map<IList<Restaurant>>(results);
            }
        }
    }
}
