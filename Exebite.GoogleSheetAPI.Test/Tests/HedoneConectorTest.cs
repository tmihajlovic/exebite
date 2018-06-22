using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.GoogleSheetAPI.Test.Mocks;
using Xunit;

namespace Exebite.GoogleSheetAPI.Test.Tests
{
    public class HedoneConectorTest
    {
        private const string RestaurantName = "Hedone";

        private readonly IHedoneConector _hedoneConector;
        private readonly IHedoneConector _hedoneConector_NullCheck;
        private readonly IHedoneConector _hedoneConector_EmptyCheck;
        private static IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        private readonly IGoogleSheetService _googleSheetService;
        private readonly IGoogleSheetService _googleSheetService_returnNull;
        private readonly IGoogleSheetService _googleSheetService_returnEmpty;
        private readonly FakeDataFactory fakeDataFactory = new FakeDataFactory(RestaurantName);

        public HedoneConectorTest()
        {
            _googleSpreadsheetIdFactory = new GoogleSpreadsheetIdFactory();
            _googleSheetService = new GoogleSheetServiceFake();
            _googleSheetService_returnNull = new GoogleSheetServiceFake_ReturnNull();
            _googleSheetService_returnEmpty = new GoogleSheetServiceFake_ReturnEmpty();
            _hedoneConector = new HedoneConector(_googleSheetService, _googleSpreadsheetIdFactory);
            _hedoneConector_NullCheck = new HedoneConector(_googleSheetService_returnNull, _googleSpreadsheetIdFactory);
            _hedoneConector_EmptyCheck = new HedoneConector(_googleSheetService_returnEmpty, _googleSpreadsheetIdFactory);
        }

        [Fact]
        public void GetDailyMenu()
        {
            var result = _hedoneConector.GetDailyMenu();
            var food = result.First();
            Assert.NotEmpty(result);
            Assert.Equal("Test food 1", food.Name);
        }

        [Fact]
        public void LoadAllFoods()
        {
            var result = _hedoneConector.LoadAllFoods();
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
            _hedoneConector.PlaceOrders(fakeDataFactory.GetOrders());
        }

        [Fact]
        public void WriteMenu()
        {
            _hedoneConector.WriteMenu(fakeDataFactory.GetFoods());
        }

        [Fact]
        public void WriteKasaTab()
        {
            _hedoneConector.WriteKasaTab(fakeDataFactory.GetCustomers());
        }

        [Fact]
        public void DnevniMenuSheetSetup()
        {
            _hedoneConector.DnevniMenuSheetSetup();
        }

        [Fact]
        public void GetDailyMenu_NullResponce()
        {
            var result = _hedoneConector_NullCheck.GetDailyMenu();
            Assert.Empty(result);
        }

        [Fact]
        public void LoadAllFoods_NullResponce()
        {
            var result = _hedoneConector_NullCheck.LoadAllFoods();
            Assert.Empty(result);
        }

        [Fact]
        public void GetDailyMenu_EmptyResponce()
        {
            var result = _hedoneConector_EmptyCheck.GetDailyMenu();
            Assert.Empty(result);
        }

        [Fact]
        public void LoadAllFoods_EmptyResponce()
        {
            var result = _hedoneConector_EmptyCheck.LoadAllFoods();
            Assert.Empty(result);
        }
    }
}
