using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public class HedoneConector : RestaurantConector, IHedoneConector
    {
        private new const string DailyMenuSheet = "Dnevni meni";
        private const string AlwaysAvailableSheet = "Uvek dostupno";
        private new const string FoodListSheet = "Cene i opis";
        private new const string OrdersSheet = "Narudzbine";

        private readonly Restaurant _restaurant;
        private readonly string _sheetId;

        public HedoneConector(IGoogleSheetService googleSheetService, IGoogleSpreadsheetIdFactory googleSSIdFactory)
            : base(googleSheetService)
        {
            _sheetId = googleSSIdFactory.GetHedone();
            _restaurant = new Restaurant { Name = "Hedone" };
            SheetId = _sheetId;
            base.OrdersSheet = OrdersSheet;
            base.DailyMenuSheet = DailyMenuSheet;
            base.FoodListSheet = FoodListSheet;
            Restaurant = _restaurant;
        }

        public override void WriteMenu(List<Food> foods)
        {
            var header = new List<object> { "Naziv jela", "Opis", "Cena", "Tip" };
            var foodRange = new ValueRange { Values = new List<IList<object>> { header } };

            foreach (var food in foods)
            {
                foodRange.Values.Add(new List<object>
                {
                    food.Name,
                    food.Description,
                    food.Price,
                    GetLocalFoodType(food.Type)
                });
            }

            // Clear sheet and write new data
            GoogleSheetService.Clear(_sheetId, FoodListSheet);
            GoogleSheetService.Update(foodRange, _sheetId, FoodListSheet);
        }

        public override List<Food> GetDailyMenu()
        {
            var allFood = new List<Food>();

            allFood.AddRange(DailyMenu());
            allFood.AddRange(AaMenu());

            return allFood;
        }

        private IEnumerable<Food> DailyMenu()
        {
            const string range = DailyMenuSheet + "!A3:A1000";
            ValueRange sheetData = GoogleSheetService.GetColumns(_sheetId, range);

            // Null and empty check
            if (sheetData?
                .Values?
                .Any() != true)
            {
                return new List<Food>();
            }

            return sheetData.Values.First().Select(f => new Food { Name = f.ToString(), Restaurant = _restaurant }).ToList();
        }

        private IEnumerable<Food> AaMenu()
        {
            const string range = AlwaysAvailableSheet + "!A2:A1000";
            ValueRange sheetData = GoogleSheetService.GetColumns(_sheetId, range);

            // Null and empty check
            if (!(sheetData?.Values?.Any() == true))
            {
                return new List<Food>();
            }

            return sheetData.Values.First().Select(f => new Food { Name = f.ToString(), Restaurant = _restaurant }).ToList();
        }
    }
}
