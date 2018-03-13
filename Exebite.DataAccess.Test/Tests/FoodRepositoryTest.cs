using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using Unity.Resolution;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class FoodRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IFoodRepository _foodRepository;
        private static IUnityContainer _container;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _container = new UnityContainer();
            Unity.UnityConfig.RegisterTypes(_container);
            _foodRepository = _container.Resolve<IFoodRepository>(new ParameterOverride("factory", _factory));
            InMemorySeed.Seed(_factory);
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
    }
}
