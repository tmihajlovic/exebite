using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LipaConectorTest
    {
        private static ILipaConector _lipaConector;
        private static IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private static IGoogleSheetService _googleSheetService;
        private static string restaurantName = "Restoran pod Lipom";
        private FakeDataFactory fakeDataFactory = new FakeDataFactory(restaurantName);

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _googleSpreadsheetIdFactory = new GoogleSpreadsheetIdFactory();
            _googleSheetService = new GoogleSheetServiceFake();
            _lipaConector = new LipaConector(_googleSheetService, _googleSpreadsheetIdFactory);
        }

        [TestMethod]
        public void GetDailyMenu()
        {
            var result = _lipaConector.GetDailyMenu();
            var food = result.First();
            Assert.AreNotEqual(result.Count, 0);
            Assert.AreEqual(food.Name, "Test food 1");
        }

        [TestMethod]
        public void LoadAllFoods()
        {
            var result = _lipaConector.LoadAllFoods();
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
            _lipaConector.PlaceOrders(fakeDataFactory.GetOrders());
        }

        [TestMethod]
        public void WriteMenu()
        {
            _lipaConector.WriteMenu(fakeDataFactory.GetFoods());
        }

        [TestMethod]
        public void WriteKasaTab()
        {
            _lipaConector.WriteKasaTab(fakeDataFactory.GetCustomers());
        }

        [TestMethod]
        public void DnevniMenuSheetSetup()
        {
            _lipaConector.DnevniMenuSheetSetup();
        }
    }
}
