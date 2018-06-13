using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
    [TestClass]
    public class HedoneConectorTest
    {
        private static IHedoneConector _hedoneConector;
        private static IHedoneConector _hedoneConector_NullCheck;
        private static IHedoneConector _hedoneConector_EmptyCheck;
        private static IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private static IGoogleSheetService _googleSheetService;
        private static IGoogleSheetService _googleSheetService_returnNull;
        private static IGoogleSheetService _googleSheetService_returnEmpty;
        private static string restaurantName = "Hedone";
        private FakeDataFactory fakeDataFactory = new FakeDataFactory(restaurantName);

        [TestInitialize]
        public void Init()
        {
            _googleSpreadsheetIdFactory = new GoogleSpreadsheetIdFactory();
            _googleSheetService = new GoogleSheetServiceFake();
            _googleSheetService_returnNull = new GoogleSheetServiceFake_ReturnNull();
            _googleSheetService_returnEmpty = new GoogleSheetServiceFake_ReturnEmpty();
            _hedoneConector = new HedoneConector(_googleSheetService, _googleSpreadsheetIdFactory);
            _hedoneConector_NullCheck = new HedoneConector(_googleSheetService_returnNull, _googleSpreadsheetIdFactory);
            _hedoneConector_EmptyCheck = new HedoneConector(_googleSheetService_returnEmpty, _googleSpreadsheetIdFactory);
        }

        [TestMethod]
        public void GetDailyMenu()
        {
            var result = _hedoneConector.GetDailyMenu();
            var food = result.First();
            Assert.AreNotEqual(result.Count, 0);
            Assert.AreEqual(food.Name, "Test food 1");
        }

        [TestMethod]
        public void LoadAllFoods()
        {
            var result = _hedoneConector.LoadAllFoods();
            var food = result.First();
            Assert.AreNotEqual(result.Count, 0);
            Assert.AreEqual(food.Name, "Test food 1");
            Assert.AreEqual(food.Description, "Description 1");
            Assert.AreEqual(food.Price, 100);
            Assert.AreEqual(food.Type, Model.FoodType.MAIN_COURSE);
        }

        [TestMethod]
        public void PlaceOrders()
        {
            _hedoneConector.PlaceOrders(fakeDataFactory.GetOrders());
        }

        [TestMethod]
        public void WriteMenu()
        {
            _hedoneConector.WriteMenu(fakeDataFactory.GetFoods());
        }

        [TestMethod]
        public void WriteKasaTab()
        {
            _hedoneConector.WriteKasaTab(fakeDataFactory.GetCustomers());
        }

        [TestMethod]
        public void DnevniMenuSheetSetup()
        {
            _hedoneConector.DnevniMenuSheetSetup();
        }

        [TestMethod]
        public void GetDailyMenu_NullResponce()
        {
            var result = _hedoneConector_NullCheck.GetDailyMenu();
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void LoadAllFoods_NullResponce()
        {
            var result = _hedoneConector_NullCheck.LoadAllFoods();
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetDailyMenu_EmptyResponce()
        {
            var result = _hedoneConector_EmptyCheck.GetDailyMenu();
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void LoadAllFoods_EmptyResponce()
        {
            var result = _hedoneConector_EmptyCheck.LoadAllFoods();
            Assert.AreEqual(result.Count, 0);
        }
    }
}
