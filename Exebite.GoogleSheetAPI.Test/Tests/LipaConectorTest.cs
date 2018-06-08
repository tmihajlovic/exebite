using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
    using Exebite.Test;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LipaConectorTest
    {
        private static ILipaConector _lipaConector;
        private static ILipaConector _lipaConector_NullCheck;
        private static ILipaConector _lipaConector_EmptyCheck;
        private static IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private static IGoogleSheetService _googleSheetService;
        private static IGoogleSheetService _googleSheetService_returnNull;
        private static IGoogleSheetService _googleSheetService_returnEmpty;
        private static string restaurantName = "Restoran pod Lipom";
        private FakeDataFactory fakeDataFactory = new FakeDataFactory(restaurantName);

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var container = ServiceProviderWrapper.GetContainer();

            _googleSheetService_returnNull = new GoogleSheetServiceFake_ReturnNull();
            _googleSheetService_returnEmpty = new GoogleSheetServiceFake_ReturnEmpty();
            _lipaConector = container.Resolve<ILipaConector>();
            _googleSheetService = container.Resolve<IGoogleSheetService>();
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
