using System.Linq;
using AutoMapper;
using Exebite.Business.GoogleApiImportExport;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
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
        private static IMapper _mapper;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {

            var container = ServiceProviderWrapper.GetContainer();

            _factory = container.Resolve<IFoodOrderingContextFactory>();

            InMemoryDBSeed.Seed(_factory);
            _lipaConector = new LipaConectorMock(_factory, _mapper);
            _hedoneConector = new HedoneConectorMock(_factory, _mapper);
            _teglasConector = new TeglasConectorMock(_factory, _mapper);
            _mapper = container.Resolve<IMapper>(); //new ExebiteMapper(ServiceProviderWrapper.GetContainer());
            _restaurantService = container.Resolve<IRestaurantService>();
            _foodService = container.Resolve<IFoodService>(); //new FoodService(new FoodRepository(_factory, _mapper));
            _googleDataImporter = new GoogleApiImport(_restaurantService, _foodService, _lipaConector, _teglasConector, _hedoneConector);
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
