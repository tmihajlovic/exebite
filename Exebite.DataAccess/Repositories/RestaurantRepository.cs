using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class RestaurantRepository : DatabaseRepository<Restaurant,RestaurantEntity> ,IRestaurantRepository
    {
        IFoodOrderingContextFactory _factory;

        public RestaurantRepository(IFoodOrderingContextFactory factory)
            :base(factory)
        {
            _factory = factory;
        }

        public Restaurant GetByName(string name)
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = context.Restaurants.Where(r => r.Name == name).FirstOrDefault();
                var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurantEntity);
                return restaurant;
            }
        }

        public override Restaurant Insert(Restaurant entity)
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(entity);

                var addedEntity = context.Restaurants.Add(restaurantEntity);
                context.SaveChanges();
                var addedModel = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(addedEntity);
                return addedModel;
            }
        }

        public override Restaurant Update(Restaurant entity)
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(entity);

                var dbRestaurant = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                context.Entry(dbRestaurant).CurrentValues.SetValues(restaurantEntity);

                List<FoodEntity> foodList = context.Foods.Where(f => f.Restaurant.Id == dbRestaurant.Id).ToList();
                //clear old menu
                dbRestaurant.DailyMenu.Clear();
                dbRestaurant.Foods.Clear();
                //bind daily food entitys
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

                //bind all food entitys
                for (int i = 0; i < restaurantEntity.Foods.Count; i++)
                {
                    var tmpfood = foodList.FirstOrDefault(f => f.Name == restaurantEntity.Foods[i].Name);
                    if (tmpfood != null)
                    {
                        dbRestaurant.Foods.Add(tmpfood);
                    }
                    else
                    {
                        restaurantEntity.Foods[i].Restaurant = dbRestaurant;
                        dbRestaurant.Foods.Add(restaurantEntity.Foods[i]);
                    }
                }


                context.SaveChanges();
                
                var resultEntity = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(resultEntity);
                return result;
            }
        }
    }
}
