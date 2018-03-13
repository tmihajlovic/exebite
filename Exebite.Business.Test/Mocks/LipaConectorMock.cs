using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.DataAccess.Migrations;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.Test.Mocks
{
    public class LipaConectorMock : ILipaConector
    {
        private IFoodOrderingContextFactory _factory;
        private string restaurantName = "Restoran pod Lipom";

        public LipaConectorMock(IFoodOrderingContextFactory factory)
        {
            _factory = factory;
        }

        public void DnevniMenuSheetSetup()
        {
        }

        public List<Food> GetDailyMenu()
        {
            List<Food> result = new List<Food>();
            using (var context = _factory.Create())
            {
                var restaurant = context.Restaurants.Single(r => r.Name == restaurantName);
                var foodEntity = context.Foods.Where(f => f.RestaurantId == restaurant.Id).ToList();
                var foodList = foodEntity.Select(f => AutoMapperHelper.Instance.GetMappedValue<Food>(f, context)).ToList();
                result.AddRange(foodList.Take(3)); // Take 3 food from all food list for daily menu
            }

            return result;
        }

        public List<Food> LoadAllFoods()
        {
            List<Food> result = new List<Food>();
            using (var context = _factory.Create())
            {
                var restaurant = context.Restaurants.Single(r => r.Name == restaurantName);
                var foodEntity = context.Foods.Where(f => f.RestaurantId == restaurant.Id).ToList();
                var foodList = foodEntity.Select(f => AutoMapperHelper.Instance.GetMappedValue<Food>(f, context)).ToList();
                result.AddRange(foodList.Take(foodList.Count - 1)); // Add one food less to be marked inactive
                var newFoodEntity = new DataAccess.Entities.FoodEntity
                {
                    Name = "Test food from conector",
                    Description = "Test food from conector description",
                    IsInactive = false,
                    Price = 100,
                    Type = Model.FoodType.MAIN_COURSE,
                    RestaurantId = 1,
                    Restaurant = context.Restaurants.Find(1)
                };
                result.Add(AutoMapperHelper.Instance.GetMappedValue<Food>(newFoodEntity, context));
            }

            return result;
        }

        public void PlaceOrders(List<Order> orders)
        {
        }

        public void WriteKasaTab(List<Customer> customerList)
        {
        }

        public void WriteMenu(List<Food> foods)
        {
        }
    }
}
