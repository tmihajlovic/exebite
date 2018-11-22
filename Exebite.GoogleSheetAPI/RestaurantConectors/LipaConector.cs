using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public class LipaConector : RestaurantConector, ILipaConector
    {
        private new const string DailyMenuSheet = "Dnevni meni";
        private new const string FoodListSheet = "Cene i opis";
        private new const string OrdersSheet = "Narudzbine";

        private readonly string _sheetId;

        private readonly Restaurant _restaurant;

        public LipaConector(IGoogleSheetService googleSheetService, IGoogleSpreadsheetIdFactory googleSSIdFactory)
            : base(googleSheetService)
        {
            _sheetId = googleSSIdFactory.GetLipa();
            _restaurant = new Restaurant { Name = "Restoran pod Lipom" };
            SheetId = _sheetId;
            base.OrdersSheet = OrdersSheet;
            base.DailyMenuSheet = DailyMenuSheet;
            base.FoodListSheet = FoodListSheet;
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
            GoogleSheetService.Clear(_sheetId, FoodListSheet);
            GoogleSheetService.Update(foodRange, _sheetId, FoodListSheet);
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
            const string range = DailyMenuSheet + "!A3:A100";
            ValueRange sheetData = GoogleSheetService.GetColumns(_sheetId, range);

            // Null and empty check
            if (!(sheetData?.Values?.Any() == true))
            {
                return new List<Food>();
            }
            else
            {
                var result = sheetData.Values.First().Select(f => new Food { Name = f.ToString(), Restaurant = _restaurant }).ToList();
                return result;
            }
        }
    }
}
