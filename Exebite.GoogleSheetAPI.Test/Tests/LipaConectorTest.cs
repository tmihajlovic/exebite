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
        private const string RestaurantName = "Restoran pod Lipom";

        private static ILipaConector _lipaConector;
        private static ILipaConector _lipaConector_NullCheck;
        private static ILipaConector _lipaConector_EmptyCheck;
        private static IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private static IGoogleSheetService _googleSheetService;
        private static IGoogleSheetService _googleSheetService_returnNull;
        private static IGoogleSheetService _googleSheetService_returnEmpty;
        private static FakeDataFactory _fakeDataFactory;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _fakeDataFactory = new FakeDataFactory(RestaurantName);
            _googleSpreadsheetIdFactory = new GoogleSpreadsheetIdFactory();
            _googleSheetService = new GoogleSheetServiceFake();
            _googleSheetService_returnNull = new GoogleSheetServiceFake_ReturnNull();
            _googleSheetService_returnEmpty = new GoogleSheetServiceFake_ReturnEmpty();
            _lipaConector = new LipaConector(_googleSheetService, _googleSpreadsheetIdFactory);
            _lipaConector_NullCheck = new LipaConector(_googleSheetService_returnNull, _googleSpreadsheetIdFactory);
            _lipaConector_EmptyCheck = new LipaConector(_googleSheetService_returnEmpty, _googleSpreadsheetIdFactory);
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
            _lipaConector.PlaceOrders(_fakeDataFactory.GetOrders());
        }

        [TestMethod]
        public void WriteMenu()
        {
            _lipaConector.WriteMenu(_fakeDataFactory.GetFoods());
        }

        [TestMethod]
        public void WriteKasaTab()
        {
            _lipaConector.WriteKasaTab(_fakeDataFactory.GetCustomers());
        }

        [TestMethod]
        public void DnevniMenuSheetSetup()
        {
            _lipaConector.DnevniMenuSheetSetup();
        }

        [TestMethod]
        public void GetDailyMenu_NullCheck()
        {
            var result = _lipaConector_NullCheck.GetDailyMenu();
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void LoadAllFoods_NullCheck()
        {
            var result = _lipaConector_NullCheck.LoadAllFoods();
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetDailyMenu_EmptyResponce()
        {
            var result = _lipaConector_EmptyCheck.GetDailyMenu();
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void LoadAllFoods_EmptyResponce()
        {
            var result = _lipaConector_EmptyCheck.LoadAllFoods();
            Assert.AreEqual(result.Count, 0);
        }
    }
}
