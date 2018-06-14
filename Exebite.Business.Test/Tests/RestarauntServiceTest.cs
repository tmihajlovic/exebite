using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class RestarauntServiceTest
    {
        private static IRestaurantRepository _restaurantRepository;

        [TestInitialize]
        public void Init()
        {
            var cointeiner = ServiceProviderWrapper.GetContainer();
            _restaurantRepository = cointeiner.Resolve<IRestaurantRepository>();
            var factory = cointeiner.Resolve<IFoodOrderingContextFactory>();
            InMemoryDBSeed.Seed(factory);
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
            const int id = 1;
            var result = _restaurantRepository.GetByID(id);
            Assert.AreEqual(result.Id, id);
        }

        [TestMethod]
        public void GetRestaurantById_NonExisting()
        {
            const int id = 0;
            var result = _restaurantRepository.GetByID(id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRestaurantByName()
        {
            const string name = "Restoran pod Lipom";
            var result = _restaurantRepository.GetByName(name);
            Assert.AreEqual(result.Name, name);
        }

        [TestMethod]
        public void GetRestaurantByName_NonExisting()
        {
            const string name = "NonExistingRestaurant";
            var result = _restaurantRepository.GetByName(name);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRestaurantByName_StringEmpty()
        {
            _restaurantRepository.GetByName(string.Empty);
        }

        [TestMethod]
        public void CreateNewRestaurant()
        {
            Restaurant newRestaurant = new Restaurant
            {
                Name = "New restaurant"
            };
            var result = _restaurantRepository.Insert(newRestaurant);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNewRestaurant_IsNull()
        {
            _restaurantRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateRestourant()
        {
            const int id = 1;
            const int foodCount = 3;
            var restaurant = _restaurantRepository.GetByID(id);
            var foods = restaurant.Foods;
            restaurant.DailyMenu.Clear();
            restaurant.DailyMenu.AddRange(foods.Take(foodCount));
            var result = _restaurantRepository.Update(restaurant);
            Assert.AreEqual(result.DailyMenu.Count, foodCount);
        }

        [TestMethod]
        public void DeleteRestourant()
        {
            const string name = "For delete";
            var restaurant = _restaurantRepository.GetByName(name);
            _restaurantRepository.Delete(restaurant.Id);
            var result = _restaurantRepository.GetByID(restaurant.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteRestourant_NonExisting()
        {
            _restaurantRepository.Delete(0);
        }
    }
}
