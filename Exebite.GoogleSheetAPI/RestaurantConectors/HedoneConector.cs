using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public class HedoneConector : RestaurantConector, IHedoneConector
    {
        private static string dailyMenuSheet = "Dnevni meni";
        private static string alwaysAvailableSheet = "Uvek dostupno";
        private static string foodListSheet = "Cene i opis";
        private static string ordersSheet = "Narudzbine";

        private Restaurant _restaurant;
        private string _sheetId;

        public HedoneConector(IGoogleSheetService googleSheetService, IGoogleSpreadsheetIdFactory googleSSIdFactory)
            : base(googleSheetService)
        {
            _sheetId = googleSSIdFactory.GetHedone();
            _restaurant = new Restaurant { Name = "Hedone" };
            SheetId = _sheetId;
            OrdersSheet = ordersSheet;
            DailyMenuSheet = dailyMenuSheet;
            FoodListSheet = foodListSheet;
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
            GoogleSheetService.Clear(_sheetId, foodListSheet);
            GoogleSheetService.Update(foodRange, _sheetId, foodListSheet);
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
            var range = dailyMenuSheet + "!A3:A1000";
            ValueRange sheetData = GoogleSheetService.GetColumns(_sheetId, range);

            // Null and empty check
            if (!(sheetData != null && sheetData.Values != null && sheetData.Values.Any()))
            {
                return new List<Food>();
            }

            return sheetData.Values.First().Select(f => new Food { Name = f.ToString(), Restaurant = _restaurant }).ToList();
        }

        private IEnumerable<Food> AaMenu()
        {
            var range = alwaysAvailableSheet + "!A2:A1000";
            ValueRange sheetData = GoogleSheetService.GetColumns(_sheetId, range);

            // Null and empty check
            if (!(sheetData != null && sheetData.Values != null && sheetData.Values.Any()))
            {
                return new List<Food>();
            }

            return sheetData.Values.First().Select(f => new Food { Name = f.ToString(), Restaurant = _restaurant }).ToList();
        }
    }
}
