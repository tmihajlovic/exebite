using System.Collections.Generic;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public sealed class HeyDayConnector : RestaurantConnector, IHeyDayConnector
    {
        private readonly string _mainMenuRange = "'MENI'!A2:C69";

        public HeyDayConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, RestaurantConstants.HEY_DAY_NAME)
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.HEY_DAY);
            ColumnsPerDay = 18;
            DailyMenuSheet = $"{GetLocalMonthName(DailyMenuDate.Month)} {DailyMenuDate.Year}";
        }

        public override void WriteMenu(List<Meal> foods)
        {
            // not needed for now. But will probably be needed in the future to write orders
            // in sheets until everything is moved to be get from DB (reports, orders...)
        }

        public List<Meal> GetMainMenu()
        {
            var allFood = new List<Meal>();
            allFood.AddRange(MainMenu());
            return allFood;
        }

        private IEnumerable<Meal> MainMenu()
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
                        case "salads":
                            foodType = MealType.SALAD;
                            break;
                        case "torillas":
                            foodType = MealType.TORTILLA;
                            break;
                        case "sandwiches":
                            foodType = MealType.SANDWICH;
                            break;
                        case "desert":
                        case "acai bowls":
                            foodType = MealType.DESSERT;
                            break;
                        case "smoothies":
                        case "wellness shots":
                            foodType = MealType.DRINK;
                            break;
                        case "dressings":
                            foodType = MealType.CONDIMENT;
                            break;
                        default:
                            foodType = MealType.MAIN_COURSE;
                            break;
                    }
                }
                else
                {
                    var priceString = row[1].ToString();
                    var parsed = decimal.TryParse(priceString, out decimal price);
                    if (!parsed)
                    {
                        decimal.TryParse(priceString.Substring(priceString.Length - 3, priceString.Length), out price);
                    }

                    result.Add(new Meal { Name = row[0].ToString(), Description = row[2].ToString(), Price = price, Restaurant = Restaurant, Type = (int)foodType });
                }
            }

            return result;
        }
    }
}
