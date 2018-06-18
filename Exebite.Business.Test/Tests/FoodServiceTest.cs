using System;
using System.Linq;
using AutoMapper;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class FoodServiceTest
    {
        private static IFoodRepository _foodRepository;
        private static IFoodOrderingContextFactory _factory;
        private static IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();
            _foodRepository = container.Resolve<IFoodRepository>();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
            _mapper = container.Resolve<IMapper>();
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllFoods()
        {
            var result = _foodRepository.Get(0, int.MaxValue);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFoodById()
        {
            const int foodId = 1;
            var result = _foodRepository.GetByID(foodId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFoodById_NonExisting()
        {
            var result = _foodRepository.GetByID(0);
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
                    Restaurant = _mapper.Map<Restaurant>(context.Restaurants.First())
                };
                var result = _foodRepository.Insert(newFood);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNewFood_IsNull()
        {
            _foodRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateFood()
        {
            using (var context = _factory.Create())
            {
                const int foodId = 1;
                const string newName = "New name";
                const string newDescription = "new description";
                const int newPrice = 300;
                var foodToUpdate = _foodRepository.GetByID(foodId);
                foodToUpdate.Description = newDescription;
                foodToUpdate.Price = newPrice;
                foodToUpdate.Name = newName;
                var result = _foodRepository.Update(foodToUpdate);
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
            _foodRepository.Update(null);
        }

        [TestMethod]
        public void DeleteFood()
        {
            const string foodName = "Test food for delete";
            var foodForDelete = _foodRepository.Get(0, int.MaxValue).Single(f => f.Name == foodName);
            _foodRepository.Delete(foodForDelete.Id);
            var result = _foodRepository.GetByID(foodForDelete.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteFood_NonExisting()
        {
            _foodRepository.Delete(0);
        }
    }
}
