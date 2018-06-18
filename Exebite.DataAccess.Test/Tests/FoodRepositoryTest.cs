using System;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.DomainModel;
using Exebite.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class FoodRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IFoodRepository _foodRepository;
        private static IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            _mapper = container.Resolve<IMapper>();
            InMemorySeed.Seed(_factory);
            _foodRepository = container.Resolve<IFoodRepository>();
        }

        [TestMethod]
        public void GetAllFood()
        {
            var result = _foodRepository.Get(0, int.MaxValue);
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
                var restaurant = _mapper.Map<Restaurant>(context.Restaurants.Find(1));
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
                var restaurant = _mapper.Map<Restaurant>(context.Restaurants.Find(1));

                var newFood = new Food()
                {
                    Name = "NewFood",
                    Description = "New Food",
                    IsInactive = false,
                    Price = 100,
                    Type = FoodType.MAIN_COURSE,
                    RestaurantId = restaurant.Id
                };
                var result = _foodRepository.Insert(newFood);
                Assert.IsNotNull(result);
                Assert.AreEqual(newFood.Name, result.Name);
                Assert.AreEqual(newFood.Description, result.Description);
                Assert.AreEqual(newFood.Price, result.Price);
                Assert.AreEqual(newFood.Type, result.Type);
                Assert.AreEqual(newFood.IsInactive, result.IsInactive);
                Assert.AreEqual(newFood.RestaurantId, result.RestaurantId);
                Assert.AreEqual(newFood.RestaurantId, result.Restaurant.Id);
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
                var food = _mapper.Map<Food>(context.Foods.Find(1));
                food.Name = "NewName";
                food.Description = "NewDesc";
                food.Price = 200;
                food.Type = FoodType.DESERT;
                food.RestaurantId = 2;
                var result = _foodRepository.Update(food);
                Assert.AreEqual(food.Name, result.Name);
                Assert.AreEqual(food.Description, result.Description);
                Assert.AreEqual(food.Price, result.Price);
                Assert.AreEqual(food.Type, result.Type);
                Assert.AreEqual(food.RestaurantId, result.Restaurant.Id);
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
                var food = _mapper.Map<Food>(context.Foods.First(f => f.Name == "Test food for delete"));
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
