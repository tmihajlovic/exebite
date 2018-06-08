using System;
using System.Linq;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Exebite.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class FoodRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IFoodRepository _foodRepository;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            InMemorySeed.Seed(_factory);
            _foodRepository = container.Resolve<IFoodRepository>();
        }

        [TestMethod]
        public void GetAllFood()
        {
            var result = _foodRepository.GetAll().ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetFoodById()
        {
            var result = _foodRepository.GetByID(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFoodById_NonExisting()
        {
            var result = _foodRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetFoodsByRestaurant()
        {
            using (var context = _factory.Create())
            {
                var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(context.Restaurants.Find(1), context);
                var result = _foodRepository.GetByRestaurant(restaurant).ToList();
                Assert.AreNotEqual(result.Count, 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFoodsByRestaurant_NonExistingRestaurant()
        {
            var resulet = _foodRepository.GetByRestaurant(null);
        }

        [TestMethod]
        public void InsertFood()
        {
            using (var context = _factory.Create())
            {
                var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(context.Restaurants.Find(1), context);

                var newFood = new Food()
                {
                    Name = "NewFood",
                    Description = "New Food",
                    IsInactive = false,
                    Price = 100,
                    Type = FoodType.MAIN_COURSE,
                    Restaurant = restaurant
                };
                var result = _foodRepository.Insert(newFood);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertFood_IsNull()
        {
            _foodRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateFood()
        {
            using (var context = _factory.Create())
            {
                var food = AutoMapperHelper.Instance.GetMappedValue<Food>(context.Foods.Find(1), context);
                food.Name = "NewName";
                food.Description = "NewDesc";
                food.Price = 200;
                food.Type = FoodType.DESERT;
                var result = _foodRepository.Update(food);
                Assert.AreEqual(result.Name, food.Name);
                Assert.AreEqual(result.Description, food.Description);
                Assert.AreEqual(result.Price, food.Price);
                Assert.AreEqual(result.Type, food.Type);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateFood_IsNull()
        {
            _foodRepository.Update(null);
        }

        [TestMethod]
        public void DeleteFood()
        {
            using (var context = _factory.Create())
            {
                var food = AutoMapperHelper.Instance.GetMappedValue<Food>(context.Foods.First(f => f.Name == "Test food for delete"), context);
                _foodRepository.Delete(food.Id);
                var result = _foodRepository.GetByID(food.Id);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void DeleteFood_NonExisting()
        {
            _foodRepository.Delete(0);
        }
    }
}
