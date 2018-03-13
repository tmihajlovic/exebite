using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exebite.Business.GoogleApiImportExport;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class GoogleDataImporterTest
    {
        // Database
        private static IFoodOrderingContextFactory _factory;

        // Services
        private static IRestaurantService _restaurantService;
        private static IFoodService _foodService;
        private static IGoogleDataImporter _googleDataImporter;

        // Conectors
        private static IHedoneConector _hedoneConector;
        private static ILipaConector _lipaConector;
        private static ITeglasConector _teglasConector;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _lipaConector = new LipaConectorMock(_factory);
            _hedoneConector = new HedoneConectorMock();
            _teglasConector = new TeglasConectorMock();
            _restaurantService = new RestaurantService(new RestaurantRepository(_factory));
            _foodService = new FoodService(new FoodRepository(_factory));
            _googleDataImporter = new GoogleApiImport(_restaurantService, _foodService, _lipaConector, _teglasConector, _hedoneConector);
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void UpdateRestorauntsMenu()
        {
            var name = "Restoran pod Lipom";
            var restaurants = _restaurantService.GetAllRestaurants();
            var lipaFoodCount = restaurants.Find(r => r.Name == name).Foods.Count;
            var lipaDailyCount = restaurants.Find(r => r.Name == name).DailyMenu.Count;

            _googleDataImporter.UpdateRestorauntsMenu();
            var lipa = _restaurantService.GetRestaurantByName(name);
            var inactiveFood = lipa.Foods.FirstOrDefault(f => f.IsInactive == true);

            // Chek if new food is added
            Assert.AreNotEqual(lipa.Foods.Count, lipaFoodCount);

            // Chek if daily manu is changed
            Assert.AreNotEqual(lipa.DailyMenu.Count, lipaDailyCount);

            // Chek if food deleted from sheet is marked inactive
            Assert.IsNotNull(inactiveFood);
        }
    }
}
