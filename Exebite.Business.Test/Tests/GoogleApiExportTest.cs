using System;
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
    public class GoogleApiExportTest
    {
        private static IGoogleDataExporter _googleDataExporter;

        // Services
        private static IOrderService _orderService;
        private static ICustomerRepository _customerRepository;
        private static IRestaurantRepository _restaurantRepository;

        // Conectors
        private static ILipaConector _lipaConector;
        private static IHedoneConector _hedoneConector;
        private static ITeglasConector _teglasConector;
        private static IMapper _mapper;

        // Database
        private static IFoodOrderingContextFactory _factory;

        [TestInitialize]
        public void Init()
        {
            var continer = ServiceProviderWrapper.GetContainer();
            _factory = new InMemoryDBFactory();
            _mapper = continer.Resolve<IMapper>();
            _orderService = new OrderService(new OrderRepository(_factory, _mapper));
            _customerRepository = new CustomerRepository(_factory, _mapper);
            _restaurantRepository = new RestaurantRepository(_factory, _mapper);
            _lipaConector = new LipaConectorMock(_factory, _mapper);
            _hedoneConector = new HedoneConectorMock(_factory, _mapper);
            _teglasConector = new TeglasConectorMock(_factory, _mapper);
            _googleDataExporter = new GoogleApiExport(_teglasConector, _hedoneConector, _lipaConector, _orderService, _customerRepository, _restaurantRepository);
            InMemoryDBSeed.Seed(_factory);
        }

        [TestMethod]
        public void PlaceOrders()
        {
            var restaurant = _restaurantRepository.GetByID(1);
            _googleDataExporter.PlaceOrdersForRestaurant(restaurant.Name);
        }

        [TestMethod]
        public void PlaceOrders_NoOrders()
        {
            var restaurant = _restaurantRepository.GetByID(2);
            _googleDataExporter.PlaceOrdersForRestaurant(restaurant.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlaceOrders_NonExistingRestaurant()
        {
            const string restaurantName = "Non Existing Restaurant";
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
