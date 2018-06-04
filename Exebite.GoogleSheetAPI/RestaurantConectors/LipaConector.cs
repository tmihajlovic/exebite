using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public class LipaConector : RestaurantConector, ILipaConector
    {
        private static string dailyMenuSheet = "Dnevni meni";
        private static string foodListSheet = "Cene i opis";
        private static string ordersSheet = "Narudzbine";

        private readonly string _sheetId;

        private Restaurant _restaurant;

        public LipaConector(IGoogleSheetService googleSheetService, IGoogleSpreadsheetIdFactory googleSSIdFactory)
            : base(googleSheetService)
        {
            _sheetId = googleSSIdFactory.GetLipa();
            _restaurant = new Restaurant { Name = "Restoran pod Lipom" };
            SheetId = _sheetId;
            OrdersSheet = ordersSheet;
            DailyMenuSheet = dailyMenuSheet;
            FoodListSheet = foodListSheet;
            Restaurant = _restaurant;
        }

        /// <summary>
        /// Write menu with all foods in DB to sheet. Used for initial writing of old menu
        /// </summary>
        /// <param name="foods">List of all food to be written</param>
        public override void WriteMenu(List<Food> foods)
        {
            // Initialize object and add header
            var header = new List<object> { "Naziv jela", "Opis", "Cena", "Tip" };
            ValueRange foodRange = new ValueRange { Values = new List<IList<object>> { header } };

            // Add food to list
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

        /// <summary>
        /// Gets food available for today
        /// </summary>
        /// <returns>List of foods</returns>
        public override List<Food> GetDailyMenu()
        {
            // Method in case always available menu is introduced
            List<Food> allFood = new List<Food>();
            allFood.AddRange(DailyMenu());
            return allFood;
        }

        /// <summary>
        /// Get food from daily menu for today
        /// </summary>
        /// <returns>List of today available food</returns>
        private IEnumerable<Food> DailyMenu()
        {
            IEnumerable<Food> dailyFood = new List<Food>();
            var range = dailyMenuSheet + "!A3:A100";
            ValueRange sheetData = GoogleSheetService.GetColumns(_sheetId, range);

            // Null and empty check
            if (!(sheetData != null && sheetData.Values != null && sheetData.Values.Any()))
            {
                return dailyFood;
            }

            dailyFood = sheetData.Values.First().Select(f => new Food { Name = f.ToString(), Restaurant = _restaurant }).ToList();
            return dailyFood;
        }
    }
}
