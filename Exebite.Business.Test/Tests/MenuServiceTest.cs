using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
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

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _menuService = new MenuService(new RestaurantRepository(_factory), new FoodRepository(_factory), new RecipeRepository(_factory));
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
    }
}
