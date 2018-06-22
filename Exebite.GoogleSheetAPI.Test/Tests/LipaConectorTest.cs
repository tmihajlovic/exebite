using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;
using Xunit;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
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

        public LipaConectorTest()
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

        [Fact]
        public void GetDailyMenu()
        {
            var result = _lipaConector.GetDailyMenu();
            var food = result.First();
            Assert.NotEmpty(result);
            Assert.Equal("Test food 1", food.Name);
        }

        [Fact]
        public void LoadAllFoods()
        {
            var result = _lipaConector.LoadAllFoods();
            var food = result.First();
            Assert.NotEmpty(result);
            Assert.Equal("Test food 1", food.Name);
            Assert.Equal("Description 1", food.Description);
            Assert.Equal(100, food.Price);
            Assert.Equal(DomainModel.FoodType.MAIN_COURSE, food.Type);
        }

        [Fact]
        public void PlaceOrders()
        {
            _lipaConector.PlaceOrders(_fakeDataFactory.GetOrders());
        }

        [Fact]
        public void WriteMenu()
        {
            _lipaConector.WriteMenu(_fakeDataFactory.GetFoods());
        }

        [Fact]
        public void WriteKasaTab()
        {
            _lipaConector.WriteKasaTab(_fakeDataFactory.GetCustomers());
        }

        [Fact]
        public void DnevniMenuSheetSetup()
        {
            _lipaConector.DnevniMenuSheetSetup();
        }

        [Fact]
        public void GetDailyMenu_NullCheck()
        {
            var result = _lipaConector_NullCheck.GetDailyMenu();
            Assert.Empty(result);
        }

        [Fact]
        public void LoadAllFoods_NullCheck()
        {
            var result = _lipaConector_NullCheck.LoadAllFoods();
            Assert.Empty(result);
        }

        [Fact]
        public void GetDailyMenu_EmptyResponce()
        {
            var result = _lipaConector_EmptyCheck.GetDailyMenu();
            Assert.Empty(result);
        }

        [Fact]
        public void LoadAllFoods_EmptyResponce()
        {
            var result = _lipaConector_EmptyCheck.LoadAllFoods();
            Assert.Empty(result);
        }
    }
}
