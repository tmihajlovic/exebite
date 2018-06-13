using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class RestarauntServiceTest
    {
        private static IRestaurantService _restaurantService;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var cointeiner = ServiceProviderWrapper.GetContainer();
            _restaurantService = cointeiner.Resolve<IRestaurantService>();
            var factory = cointeiner.Resolve<IFoodOrderingContextFactory>();
            InMemoryDBSeed.Seed(factory);
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
            const int id = 1;
            var result = _restaurantService.GetRestaurantById(id);
            Assert.AreEqual(result.Id, id);
        }

        [TestMethod]
        public void GetRestaurantById_NonExisting()
        {
            const int id = 0;
            var result = _restaurantService.GetRestaurantById(id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRestaurantByName()
        {
            const string name = "Restoran pod Lipom";
            var result = _restaurantService.GetRestaurantByName(name);
            Assert.AreEqual(result.Name, name);
        }

        [TestMethod]
        public void GetRestaurantByName_NonExisting()
        {
            const string name = "NonExistingRestaurant";
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
            const int id = 1;
            const int foodCount = 3;
            var restaurant = _restaurantService.GetRestaurantById(id);
            var foods = restaurant.Foods;
            restaurant.DailyMenu.Clear();
            restaurant.DailyMenu.AddRange(foods.Take(foodCount));
            var result = _restaurantService.UpdateRestaurant(restaurant);
            Assert.AreEqual(result.DailyMenu.Count, foodCount);
        }

        [TestMethod]
        public void DeleteRestourant()
        {
            const string name = "For delete";
            var restaurant = _restaurantService.GetRestaurantByName(name);
            _restaurantService.DeleteRestaurant(restaurant.Id);
            var result = _restaurantService.GetRestaurantById(restaurant.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteRestourant_NonExisting()
        {
            _restaurantService.DeleteRestaurant(0);
        }
    }
}
