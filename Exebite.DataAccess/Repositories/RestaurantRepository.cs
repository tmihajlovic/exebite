using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class RestaurantRepository : DatabaseRepository<Restaurant, RestaurantEntity>, IRestaurantRepository
    {
        private readonly IFoodOrderingContextFactory _factory;

        public RestaurantRepository(IFoodOrderingContextFactory factory)
            : base(factory)
        {
            _factory = factory;
        }

        public Restaurant GetByName(string name)
        {
            if (name == string.Empty)
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

                return AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurantEntity, context);
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
                var restaurantEntity = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(entity, context);

                var addedEntity = context.Restaurants.Add(restaurantEntity).Entity;
                context.SaveChanges();
                return AutoMapperHelper.Instance.GetMappedValue<Restaurant>(addedEntity, context);
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
                var restaurantEntity = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(entity, context);

                var dbRestaurant = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                context.Entry(dbRestaurant).CurrentValues.SetValues(restaurantEntity);

                List<FoodEntity> foodList = context.Foods.Where(f => f.Restaurant.Id == dbRestaurant.Id).ToList();

                // clear old menu
                dbRestaurant.DailyMenu.Clear();

                // bind daily food entities
                for (int i = 0; i < restaurantEntity.DailyMenu.Count; i++)
                {
                    var tmpfood = foodList.FirstOrDefault(f => f.Name == restaurantEntity.DailyMenu[i].Name);
                    if (tmpfood != null)
                    {
                        dbRestaurant.DailyMenu.Add(tmpfood);
                    }
                    else
                    {
                        restaurantEntity.DailyMenu[i].Restaurant = dbRestaurant;
                        dbRestaurant.DailyMenu.Add(restaurantEntity.DailyMenu[i]);
                    }
                }

                context.SaveChanges();

                var resultEntity = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                return AutoMapperHelper.Instance.GetMappedValue<Restaurant>(resultEntity, context);
            }
        }
    }
}
