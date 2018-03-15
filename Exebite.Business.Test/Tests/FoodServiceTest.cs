using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class FoodServiceTest
    {
        private static IFoodService _foodService;
        private static IFoodOrderingContextFactory _factory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _foodService = new FoodService(new FoodRepository(_factory));
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllFoods()
        {
            var result = _foodService.GetAllFoods();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFoodById()
        {
            var foodId = 1;
            var result = _foodService.GetFoodById(foodId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFoodById_NonExisting()
        {
            var result = _foodService.GetFoodById(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CrateNewFood()
        {
            using (var context = _factory.Create())
            {
                Food newFood = new Food
                {
                    Name = "New food",
                    Description = "Food to be created",
                    IsInactive = false,
                    Price = 200,
                    Type = FoodType.MAIN_COURSE,
                    Restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(context.Restaurants.First(), context)
                };
                var result = _foodService.CreateNewFood(newFood);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNewFood_IsNull()
        {
            _foodService.CreateNewFood(null);
        }

        [TestMethod]
        public void UpdateFood()
        {
            using (var context = _factory.Create())
            {
                var foodId = 1;
                var newName = "New name";
                var newDescription = "new description";
                var newPrice = 300;
                var foodToUpdate = _foodService.GetFoodById(foodId);
                foodToUpdate.Description = newDescription;
                foodToUpdate.Price = newPrice;
                foodToUpdate.Name = newName;
                var result = _foodService.UpdateFood(foodToUpdate);
                Assert.AreEqual(result.Name, newName);
                Assert.AreEqual(result.Description, newDescription);
                Assert.AreEqual(result.Price, newPrice);
                Assert.AreEqual(result.Id, foodId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateFood_IsNull()
        {
            _foodService.UpdateFood(null);
        }

        [TestMethod]
        public void DeleteFood()
        {
            var foodName = "Test food for delete";
            var foodForDelete = _foodService.GetAllFoods().Single(f => f.Name == foodName);
            _foodService.Delete(foodForDelete.Id);
            var result = _foodService.GetFoodById(foodForDelete.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteFood_NonExisting()
        {
            _foodService.Delete(0);
        }
    }
}
