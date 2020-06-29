using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Common;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public sealed class ParrillaConnector : RestaurantConnector, IParrillaConnector
    {
        private readonly string _mainMenuRange = "'Opisi jela i cene'!A2:C31";

        public ParrillaConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, RestaurantConstants.PARRILLA_NAME)
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.PARRILLA);
            DailyMenuSheet = GetLocalMonthName(DateTime.Now.Month) + DateTime.Now.Year;
        }

        public override void WriteMenu(List<Meal> foods)
        {
            // not needed for now. But will probably be needed in the future to write orders
            // in sheets until everything is moved to be get from DB (reports, orders...)
        }

        public List<Meal> GetDailyMenu()
        {
            var allFood = new List<Meal>();
            allFood.AddRange(DailyMenu());
            return allFood;
        }

        public List<Meal> GetMainMenu()
        {
            var allFood = new List<Meal>();
            allFood.AddRange(MainMenu());
            return allFood;
        }

        private IEnumerable<Meal> DailyMenu()
        {
            var date = DateTime.Today;
            var foundMerge = FindDateRangeInSheets(date);

            if (foundMerge.IsSuccess)
            {
                var namesRange = A1Notation.ToRangeFormat(
                        startColumn: foundMerge.Value.Range.StartColumnIndex.Value,
                        startRow: 0, // Start Corner
                        endColumn: foundMerge.Value.Range.EndColumnIndex.Value,
                        endRow: 4); // End corner

                var offersList = GoogleSheetService.ReadSheetData(string.Format("'{0}'!{1}", foundMerge.Value.SheetName, namesRange), SheetId);

                var result = new List<Meal>();
                var foodNames = offersList.Values.First().ToList();
                var foodPrices = offersList.Values.Last().ToList();
                MealType foodType;

                for (int i = 0; i < foodPrices.Count; i++)
                {
                    var foodName = foodNames.ElementAt(i).ToString();
                    var foodPrice = foodPrices.ElementAt(i).ToString();

                    if (!string.IsNullOrWhiteSpace(foodName) &&
                        !string.IsNullOrWhiteSpace(foodPrice) &&
                        decimal.TryParse(foodPrice, out decimal price))
                    {
                        if (foodName.Contains("salata") && price < 100)
                        {
                            foodType = MealType.SALAD;
                        }
                        else
                        {
                            foodType = MealType.MAIN_COURSE;
                        }

                        result.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)foodType });
                    }
                }

                return result;
            }
            else
            {
                return new List<Meal>();
            }
        }

        private List<Meal> MainMenu()
        {
            var offersList = GoogleSheetService.ReadSheetData(_mainMenuRange, SheetId);
            var result = new List<Meal>();
            var foodType = MealType.MAIN_COURSE;

            foreach (var row in offersList.Values)
            {
                if (row.Count == 1)
                {
                    switch (row[0].ToString())
                    {
                        case "Obrok salate":
                            foodType = MealType.SALAD;
                            break;
                        case "Burgeri":
                            foodType = MealType.BURGER;
                            break;
                        case "Sendviči":
                            foodType = MealType.SANDWICH;
                            break;
                        case "Desert":
                            foodType = MealType.DESSERT;
                            break;
                        case "Dodaci":
                            foodType = MealType.SIDE_DISH;
                            break;
                        default:
                            foodType = MealType.MAIN_COURSE;
                            break;
                    }
                }
                else
                {
                    var priceString = row[2].ToString();
                    var parsed = decimal.TryParse(priceString.Substring(0, priceString.Length - 4), out decimal price);
                    if (!parsed)
                    {
                        price = 0;
                    }

                    result.Add(new Meal { Name = row[0].ToString(), Description = row[1].ToString(), Price = price, Restaurant = Restaurant, IsFromStandardMenu = true, Type = (int)foodType });
                }
            }

            return result;
        }
    }
}
