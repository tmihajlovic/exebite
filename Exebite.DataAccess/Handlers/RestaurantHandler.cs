using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Handlers;
using Exebite.Model;

namespace Exebite.DataAccess.Handlers
{
    public class RestaurantHandler :DatabaseHandler<Restaurant> ,IRestaurantHandler
    {
        IFoodOrderingContextFactory _factory;

        public RestaurantHandler(IFoodOrderingContextFactory factory):base(factory)
        {
            _factory = factory;
        }

        //public void Delete(int Id)
        //{
        //    using (var context = _factory.Create())
        //    {
        //        var restaurant = context.Restaurants.Find(Id);
        //        context.Restaurants.Remove(restaurant);
        //        context.SaveChanges();
        //    }
        //}

        //public IEnumerable<Restaurant> Get()
        //{
        //    using (var context = _factory.Create())
        //    {
        //        var restaurantEntities = new List<Restaurant>();

        //        foreach (var restaurant in context.Restaurants)
        //        {
        //            var restaurantModel = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurant);
        //            restaurantEntities.Add(restaurantModel);
        //        }

        //        return restaurantEntities;
        //    }
        //}

        //public Restaurant GetByID(int Id)
        //{
        //    using (var context = _factory.Create())
        //    {
        //        var restaurantEntity = context.Restaurants.Find(Id);
        //        var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurantEntity);
        //        return restaurant;
        //    }
        //}

        public Restaurant GetByName(string name)
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = context.Restaurants.Where(r => r.Name == name).FirstOrDefault();
                var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurantEntity);
                return restaurant;
            }
        }

        public void Insert(Restaurant entity)
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(entity);

                context.Restaurants.Add(restaurantEntity);
                context.SaveChanges();
            }
        }

        public void Update(Restaurant entity)
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(entity);

                var dbRestaurant = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                context.Entry(dbRestaurant).CurrentValues.SetValues(restaurantEntity);

                List<FoodEntity> foodList = context.Foods.Where(f => f.Restaurant.Id == dbRestaurant.Id).ToList();
                //clear old menu
                dbRestaurant.DailyMenu.Clear();

                //bind food entitys
                for (int i=0; i < restaurantEntity.DailyMenu.Count; i++)
                {
                    var tmpfood = foodList.FirstOrDefault(f => f.Name == restaurantEntity.DailyMenu[i].Name);
                    if(tmpfood != null)
                    {
                        dbRestaurant.DailyMenu.Add(tmpfood);
                    }
                    else
                    {
                        dbRestaurant.DailyMenu.Add(restaurantEntity.DailyMenu[i]);
                    }
                }
                context.SaveChanges();
                
                var result = context.Restaurants.FirstOrDefault(r => r.Id == entity.Id);
                //return result;
                //context.Entry(restaurantEntity).State = EntityState.Modified;
                //context.SaveChanges();
            }
        }
    }
}
