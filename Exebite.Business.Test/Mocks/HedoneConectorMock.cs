using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.DataAccess.Migrations;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.Test.Mocks
{
    public class HedoneConectorMock : IHedoneConector
    {
        private IFoodOrderingContextFactory _factory;
        private string restaurantName = "Hedone";

        public HedoneConectorMock(IFoodOrderingContextFactory factory)
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
                    Name = "Test food from connector for: " + restaurant.Name,
                    Description = "Test food from connector description",
                    IsInactive = false,
                    Price = 100,
                    Type = FoodType.MAIN_COURSE,
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant
                };
                result.Add(AutoMapperHelper.Instance.GetMappedValue<Food>(newFoodEntity, context));
            }

            return result;
        }

        public void PlaceOrders(List<Order> orders)
        {
            if (orders == null)
            {
                throw new Exception("Data is null");
            }
        }

        public void WriteKasaTab(List<Customer> customerList)
        {
            if (customerList == null)
            {
                throw new Exception("Data is null");
            }
        }

        public void WriteMenu(List<Food> foods)
        {
            if (foods == null)
            {
                throw new Exception("Data is null");
            }
        }
    }
}
