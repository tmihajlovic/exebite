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
    public class MenuServiceTest
    {
        private static IMenuService _menuService;
        private static IFoodOrderingContextFactory _factory;
        private static IExebiteMapper _mapper;


        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _menuService = new MenuService(new RestaurantRepository(_factory,_mapper), new FoodRepository(_factory, _mapper), new RecipeRepository(_factory, _mapper));
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void GetRestorantsWithMenus()
        {
            var result = _menuService.GetRestorantsWithMenus();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CheckAvailableSideDishes()
        {
            var food = _menuService.GetRestorantsWithMenus().First().Foods.First();
            var result = _menuService.CheckAvailableSideDishes(food.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckAvailableSideDishes_FoodNonExisting()
        {
            _menuService.CheckAvailableSideDishes(0);
        }

        [TestMethod]
        public void CheckAvailableSideDishes_NoSideDishes()
        {
            var food = _menuService.GetRestorantsWithMenus().First().Foods.Last();
            var result = _menuService.CheckAvailableSideDishes(food.Id);
            Assert.AreEqual(result.Count, 0);
        }
    }
}
