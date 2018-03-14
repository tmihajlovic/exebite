using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
    [TestClass]
    public class TeglasConectorTest
    {
        private static ITeglasConector _teglasConector;
        private static IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private static IGoogleSheetService _googleSheetService;
        private static string restaurantName = "Hedone";
        private FakeDataFactory fakeDataFactory = new FakeDataFactory(restaurantName);

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _googleSpreadsheetIdFactory = new GoogleSpreadsheetIdFactory();
            _googleSheetService = new GoogleSheetServiceFake();
            _teglasConector = new TeglasConector(_googleSheetService, _googleSpreadsheetIdFactory);
        }

        [TestMethod]
        public void GetDailyMenu()
        {
            var result = _teglasConector.GetDailyMenu();
            var food = result.First();
            Assert.AreNotEqual(result.Count, 0);
            Assert.AreEqual(food.Name, "Test aa food 1");
        }

        [TestMethod]
        public void LoadAllFoods()
        {
            var result = _teglasConector.LoadAllFoods();
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
            _teglasConector.PlaceOrders(fakeDataFactory.GetOrders());
        }

        [TestMethod]
        public void WriteMenu()
        {
            _teglasConector.WriteMenu(fakeDataFactory.GetFoods());
        }

        [TestMethod]
        public void WriteKasaTab()
        {
            _teglasConector.WriteKasaTab(fakeDataFactory.GetCustomers());
        }
    }
}
