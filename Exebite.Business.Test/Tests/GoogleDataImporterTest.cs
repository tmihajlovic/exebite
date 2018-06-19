using System.Linq;
using AutoMapper;
using Exebite.Business.GoogleApiImportExport;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Context;
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
        private static IRestaurantRepository _restaurantRepository;
        private static IFoodRepository _foodRepository;
        private static IGoogleDataImporter _googleDataImporter;

        // Connectors
        private static IHedoneConector _hedoneConector;
        private static ILipaConector _lipaConector;
        private static ITeglasConector _teglasConector;
        private static IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            var container = ServiceProviderWrapper.GetContainer();

            _factory = container.Resolve<IFoodOrderingContextFactory>();

            InMemoryDBSeed.Seed(_factory);
            _lipaConector = new LipaConectorMock(_factory, _mapper);
            _hedoneConector = new HedoneConectorMock(_factory, _mapper);
            _teglasConector = new TeglasConectorMock(_factory, _mapper);
            _mapper = container.Resolve<IMapper>();
            _restaurantRepository = container.Resolve<IRestaurantRepository>();
            _foodRepository = container.Resolve<IFoodRepository>();
            _googleDataImporter = new GoogleApiImport(_restaurantRepository, _foodRepository, _lipaConector, _teglasConector, _hedoneConector);
        }

        [TestMethod]
        [Ignore("This should be changed when we start updating complexed types")]
        public void UpdateRestorauntsMenu()
        {
            const string name = "Restoran pod Lipom";
            var restaurants = _restaurantRepository.Get(0, int.MaxValue);
            var lipaFoodCount = restaurants.FirstOrDefault(r => r.Name == name).Foods.Count;
            var lipaDailyCount = restaurants.FirstOrDefault(r => r.Name == name).DailyMenu.Foods.Count;

            _googleDataImporter.UpdateRestorauntsMenu();
            var lipa = _restaurantRepository.Query(new RestaurantQueryModel { Name = name }).FirstOrDefault();
            var inactiveFood = lipa.Foods.FirstOrDefault(f => f.IsInactive);

            // Check if new food is added
            Assert.AreNotEqual(lipa.Foods.Count, lipaFoodCount);

            // Check if daily menu is changed
            Assert.AreNotEqual(lipa.DailyMenu.Foods.Count, lipaDailyCount);

            // Check if food deleted from sheet is marked inactive
            Assert.IsNotNull(inactiveFood);
        }
    }
}
