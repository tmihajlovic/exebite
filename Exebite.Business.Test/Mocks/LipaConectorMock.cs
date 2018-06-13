using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Migrations;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.Test.Mocks
{
    public class LipaConectorMock : ILipaConector
    {
        private IFoodOrderingContextFactory _factory;
        private IMapper _mapper;
        private string restaurantName = "Restoran pod Lipom";

        public LipaConectorMock(IFoodOrderingContextFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
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
                var foodList = foodEntity.Select(f => _mapper.Map<Food>(f)).ToList();
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
                var foodList = foodEntity.Select(f => _mapper.Map<Food>(f)).ToList();
                result.AddRange(foodList.Take(foodList.Count - 1)); // Add one food less to be marked inactive
                var newFoodEntity = new DataAccess.Entities.FoodEntity
                {
                    Name = "Test food from conector for: " + restaurant.Name,
                    Description = "Test food from conector description",
                    IsInactive = false,
                    Price = 100,
                    Type = Model.FoodType.MAIN_COURSE,
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant
                };
                result.Add(_mapper.Map<Food>(newFoodEntity));
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
