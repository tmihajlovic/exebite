using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class RestarauntServiceTest
    {
        private static IRestaurantService _restaurantService;
        private static IFoodOrderingContextFactory _factory;
        private static IExebiteMapper _mapper;


        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _restaurantService = new RestaurantService(new RestaurantRepository(_factory, _mapper));
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllRestaurants()
        {
            var result = _restaurantService.GetAllRestaurants();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetRestaurantById()
        {
            var id = 1;
            var result = _restaurantService.GetRestaurantById(id);
            Assert.AreEqual(result.Id, id);
        }

        [TestMethod]
        public void GetRestaurantById_NonExisting()
        {
            var id = 0;
            var result = _restaurantService.GetRestaurantById(id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRestaurantByName()
        {
            var name = "Restoran pod Lipom";
            var result = _restaurantService.GetRestaurantByName(name);
            Assert.AreEqual(result.Name, name);
        }

        [TestMethod]
        public void GetRestaurantByName_NonExisting()
        {
            var name = "NonExistingRestaurant";
            var result = _restaurantService.GetRestaurantByName(name);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRestaurantByName_StringEmpty()
        {
             _restaurantService.GetRestaurantByName(string.Empty);
        }

        [TestMethod]
        public void CreateNewRestaurant()
        {
            Restaurant newRestaurant = new Restaurant
            {
                Name = "New restaurant"
            };
            var result = _restaurantService.CreateNewRestaurant(newRestaurant);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNewRestaurant_IsNull()
        {
            _restaurantService.CreateNewRestaurant(null);
        }

        [TestMethod]
        public void UpdateRestourant()
        {
            var id = 1;
            var foodCount = 3;
            var restaurant = _restaurantService.GetRestaurantById(id);
            var foods = restaurant.Foods;
            restaurant.DailyMenu.Clear();
            restaurant.DailyMenu.AddRange(foods.Take(foodCount));
            var result = _restaurantService.UpdateRestourant(restaurant);
            Assert.AreEqual(result.DailyMenu.Count, foodCount);
        }

        [TestMethod]
        public void DeleteRestourant()
        {
            var name = "For delete";
            var restaurant = _restaurantService.GetRestaurantByName(name);
            _restaurantService.DeleteRestourant(restaurant.Id);
            var result = _restaurantService.GetRestaurantById(restaurant.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteRestourant_NonExisting()
        {
            _restaurantService.DeleteRestourant(0);
        }
    }
}
