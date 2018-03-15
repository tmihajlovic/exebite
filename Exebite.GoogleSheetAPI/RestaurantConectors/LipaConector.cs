using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public class LipaConector : RestaurantConector, ILipaConector
    {
        private static string dailyMenuSheet = "Dnevni meni";
        private static string foodListSheet = "Cene i opis";
        private static string ordersSheet = "Narudzbine";

        private Restaurant _restaurant;
        private string _sheetId;

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
        /// Vrite menu with all foods in DB to sheet. Used for initial writing of old menu
        /// </summary>
        /// <param name="foods">List of all food to be writen</param>
        public override void WriteMenu(List<Food> foods)
        {
            // Initaize object and add header
            List<object> header = new List<object> { "Naziv jela", "Opis", "Cena", "Tip" };
            ValueRange foodRange = new ValueRange();
            foodRange.Values = new List<IList<object>>();
            foodRange.Values.Add(header);

            // Add food to list
            foreach (var food in foods)
            {
                List<object> foodData = new List<object>();
                foodData.Add(food.Name);
                foodData.Add(food.Description);
                foodData.Add(food.Price);
                foodData.Add(GetLocalFoodType(food.Type));
                foodRange.Values.Add(foodData);
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
            // Method in case always available menu is interduced
            List<Food> allFood = new List<Food>();
            allFood.AddRange(DailyMenu());
            return allFood;
        }

        /// <summary>
        /// Get food from daily menu for today
        /// </summary>
        /// <returns>List of toaday available foof</returns>
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
