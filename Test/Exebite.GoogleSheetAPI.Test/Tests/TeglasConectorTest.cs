using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;
using Xunit;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
    public class TeglasConectorTest
    {
        private const string RestaurantName = "Hedone";
        private readonly ITeglasConector _teglasConector;
        private readonly ITeglasConector _teglasConector_NullCheck;
        private readonly ITeglasConector _teglasConector_EmptyCheck;
        private readonly IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private readonly IGoogleSheetService _googleSheetService;
        private readonly IGoogleSheetService _googleSheetService_returnNull;
        private readonly IGoogleSheetService _googleSheetService_returnEmpty;
        private readonly FakeDataFactory _fakeDataFactory;

        public TeglasConectorTest()
        {
            _fakeDataFactory = new FakeDataFactory(RestaurantName);
            _googleSpreadsheetIdFactory = new GoogleSpreadsheetIdFactory();
            _googleSheetService = new GoogleSheetServiceFake();
            _googleSheetService_returnNull = new GoogleSheetServiceFake_ReturnNull();
            _googleSheetService_returnEmpty = new GoogleSheetServiceFake_ReturnEmpty();
            _teglasConector = new TeglasConector(_googleSheetService, _googleSpreadsheetIdFactory);
            _teglasConector_NullCheck = new TeglasConector(_googleSheetService_returnNull, _googleSpreadsheetIdFactory);
            _teglasConector_EmptyCheck = new TeglasConector(_googleSheetService_returnEmpty, _googleSpreadsheetIdFactory);
        }

        [Fact]
        public void GetDailyMenu()
        {
            var result = _teglasConector.GetDailyMenu();
            var food = result.First();
            Assert.NotEmpty(result);
            Assert.Equal("Test aa food 1", food.Name);
        }

        [Fact]
        public void LoadAllFoods()
        {
            var result = _teglasConector.LoadAllFoods();
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
            _teglasConector.PlaceOrders(_fakeDataFactory.GetOrders());
        }

        [Fact]
        public void WriteMenu()
        {
            _teglasConector.WriteMenu(_fakeDataFactory.GetFoods());
        }

        [Fact]
        public void WriteKasaTab()
        {
            _teglasConector.WriteKasaTab(_fakeDataFactory.GetCustomers());
        }

        [Fact]
        public void GetDailyMenu_NullResponce()
        {
            var result = _teglasConector_NullCheck.GetDailyMenu();
            Assert.Empty(result);
        }

        [Fact]
        public void LoadAllFoods_NullResponce()
        {
            var result = _teglasConector_NullCheck.LoadAllFoods();
            Assert.Empty(result);
        }

        [Fact]
        public void GetDailyMenu_EmptyResponce()
        {
            var result = _teglasConector_EmptyCheck.GetDailyMenu();
            Assert.Empty(result);
        }

        [Fact]
        public void LoadAllFoods_EmptyResponce()
        {
            var result = _teglasConector_EmptyCheck.LoadAllFoods();
            Assert.Empty(result);
        }
    }
}
