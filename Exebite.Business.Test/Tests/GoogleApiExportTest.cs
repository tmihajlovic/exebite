using System;
using System.Linq;
using Exebite.Business.GoogleApiImportExport;
using Exebite.Business.Test.Mocks;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.Business.Test.Tests
{
    [TestClass]
    public class GoogleApiExportTest
    {
        private static IGoogleDataExporter _googleDataExporter;

        // Services
        private static IOrderService _orderService;
        private static ICustomerService _customerService;
        private static IRestaurantService _restaurantService;

        // Conectors
        private static ILipaConector _lipaConector;
        private static IHedoneConector _hedoneConector;
        private static ITeglasConector _teglasConector;

        // Database
        private static IFoodOrderingContextFactory _factory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            _orderService = new OrderService(new OrderRepository(_factory));
            _customerService = new CustomerService(new CustomerRepository(_factory));
            _restaurantService = new RestaurantService(new RestaurantRepository(_factory));
            _lipaConector = new LipaConectorMock(_factory);
            _hedoneConector = new HedoneConectorMock(_factory);
            _teglasConector = new TeglasConectorMock(_factory);
            _googleDataExporter = new GoogleApiExport(_teglasConector, _hedoneConector, _lipaConector, _orderService, _customerService,  _restaurantService);
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void PlaceOrders()
        {
            var restaurant = _restaurantService.GetRestaurantById(1);
            _googleDataExporter.PlaceOrdersForRestaurant(restaurant.Name);
        }

        [TestMethod]
        public void PlaceOrders_NoOrders()
        {
            var restaurant = _restaurantService.GetRestaurantById(2);
            _googleDataExporter.PlaceOrdersForRestaurant(restaurant.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlaceOrders_NonExistingRestaurant()
        {
            var restaurantName = "Non Existing Restaurant";
            _googleDataExporter.PlaceOrdersForRestaurant(restaurantName);
        }

        [TestMethod]
        public void SetupDailyMenuDayOrder()
        {
            _googleDataExporter.SetupDailyMenuDayOrder();
        }

        [TestMethod]
        public void UpdateKasaTab()
        {
            _googleDataExporter.UpdateKasaTab();
        }
    }
}
