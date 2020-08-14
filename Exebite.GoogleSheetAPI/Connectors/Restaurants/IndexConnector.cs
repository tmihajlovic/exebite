using System.Collections.Generic;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public sealed class IndexConnector : RestaurantConnector, IIndexConnector
    {
        private readonly string _mainMenuRange = "'Opisi jela i cene'!A3:D118";

        public IndexConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, RestaurantConstants.INDEX_NAME)
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.INDEX_HOUSE);
            ColumnsPerDay = 13;
            DailyMenuSheet = $"Narucivanje-{GetLocalMonthName(DailyMenuDate.Month)}";
        }

        /// <summary>
        /// Write menu with all foods in DB to sheet. Used for initial writing of old menu
        /// </summary>
        /// <param name="foods">List of all food to be written</param>
        public override void WriteMenu(List<Meal> foods)
        {
            // not needed for now. But will probably be needed in the future to write orders
            // in sheets until everything is moved to be get from DB (reports, orders...)
        }

        /// <summary>
        /// Gets meals from the main menu
        /// </summary>
        /// <returns>List of meals</returns>
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
            var gillSandwichesAndPljeskavice = new List<Meal>();
            var mealSalads = new List<Meal>();
            var mealCondiments = new List<Meal>();
            var saladCondiments = new List<Meal>();
            var foodCategory = string.Empty;
            string foodName;
            MealType foodType;

            foreach (var row in offersList.Values)
            {
                if (row.Count > 2)
                {
                    foodName = row[1].ToString();

                    if (!string.IsNullOrWhiteSpace(row[0].ToString()))
                    {
                        foodCategory = row[0].ToString();
                    }

                    var parsed = decimal.TryParse(row[2].ToString(), out decimal price);
                    if (!parsed)
                    {
                        var strLength = row[2].ToString().Length;
                        decimal.TryParse(row[2].ToString().Substring(0, strLength - 4), out price);
                    }

                    if (!foodCategory.StartsWith("DODACI"))
                    {
                        switch (foodCategory)
                        {
                            case "BURGERI":
                            case "PLJESKAVICE":
                                foodType = MealType.BURGER;
                                gillSandwichesAndPljeskavice.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)foodType });
                                continue;
                            case "GRILL SENDVIČI":
                                foodType = MealType.SANDWICH;
                                gillSandwichesAndPljeskavice.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)foodType });
                                continue;
                            case "GOTOVI SENDVIČI":
                                foodType = MealType.SANDWICH;
                                break;
                            case "OBROK SALATE":
                                foodType = MealType.SALAD;
                                mealSalads.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)foodType });
                                continue;
                            case "CHICKEN NUGGETS":
                                foodType = MealType.CHICKEN;
                                foodName = "Chicken Nuggets - " + foodName;
                                break;
                            case "VEGETERIJANSKA I POSNA JELA":
                                foodType = MealType.VEGETARIAN;
                                break;
                            case "PASTE":
                                foodType = MealType.PASTA;
                                break;
                            case "JELA OD PILETINE":
                                foodType = MealType.CHICKEN;
                                break;
                            default:
                                foodType = MealType.MAIN_COURSE;
                                break;
                        }

                        result.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)foodType });
                    }
                    else
                    {
                        if (foodCategory.EndsWith("SALATE"))
                        {
                            saladCondiments.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)MealType.CONDIMENT });
                        }
                        else
                        {
                            mealCondiments.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant, Type = (int)MealType.CONDIMENT });
                        }
                    }
                }
            }

            foreach (var meal in gillSandwichesAndPljeskavice)
            {
                meal.Condiments = mealCondiments;
            }

            foreach (var salad in mealSalads)
            {
                salad.Condiments = saladCondiments;
            }

            result.AddRange(gillSandwichesAndPljeskavice);
            result.AddRange(mealSalads);

            return result;
        }
    }
}
