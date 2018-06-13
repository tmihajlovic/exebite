using System;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Exebite.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class RestaurantRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IRestaurantRepository _restaurantRepository;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            InMemorySeed.Seed(_factory);

            _restaurantRepository = container.Resolve<IRestaurantRepository>();
        }

        [TestMethod]
        public void GetAllRestaurants()
        {
            var result = _restaurantRepository.Get(0, int.MaxValue);
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetRestaurantById()
        {
            var result = _restaurantRepository.GetByID(1);
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void GetRestaurantById_NonExisting()
        {
            var result = _restaurantRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRestaurantByName()
        {
            var result = _restaurantRepository.GetByName("Teglas");
            Assert.AreEqual(result.Name, "Teglas");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRestaurantByName_NameEmpty()
        {
            var result = _restaurantRepository.GetByName(string.Empty);
        }

        [TestMethod]
        public void GetRestaurantByName_NonExisting()
        {
            var result = _restaurantRepository.GetByName("Non existing restauran");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void InsertRestaurant()
        {
            Restaurant newRestaurant = new Restaurant
            {
                Name = "NewRestaurant"
            };
            _restaurantRepository.Insert(newRestaurant);
            var result = _restaurantRepository.GetByName(newRestaurant.Name);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertRestaurant_IsNull()
        {
            _restaurantRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateRestaurantDailyMenu()
        {
            var restaurant = _restaurantRepository.GetByID(1);
            var foodToAdd = restaurant.Foods;
            restaurant.DailyMenu.AddRange(foodToAdd);
            var result = _restaurantRepository.Update(restaurant);
            Assert.AreEqual(result.DailyMenu.Count, foodToAdd.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRestaurantDailyMenu_IsNull()
        {
            var result = _restaurantRepository.Update(null);
        }

        [TestMethod]
        public void RemoveRestaurant()
        {
            var restaurant = _restaurantRepository.GetByName("For delete");
            _restaurantRepository.Delete(restaurant.Id);
            var result = _restaurantRepository.GetByName("For delete");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RemoveRestaurant_NonExisting()
        {
            _restaurantRepository.Delete(0);
        }
    }
}
