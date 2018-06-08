using System;
using System.Linq;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
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
            var container = ServiceProviderWrapper.GetContainer();
            _menuService = container.Resolve<IMenuService>();
            _factory = container.Resolve<IFoodOrderingContextFactory>();
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
