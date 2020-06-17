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
    public sealed class TopliObrokConnector : RestaurantConnector, ITopliObrokConnector
    {
        public TopliObrokConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, RestaurantConstants.TOPLI_OBROK_NAME)
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.TOPLI_OBROK);
            DailyMenuSheet = GetLocalMonthName(DateTime.Now.Month) + DateTime.Now.Year;
        }

        /// <summary>
        /// Write menu with all meals in DB to sheet. Used for initial writing of old menu
        /// </summary>
        /// <param name="meals">List of all food to be written</param>
        public override void WriteMenu(List<Meal> meals)
        {
            // not needed for now. But will probably be needed in the future to write orders
            // in sheets until everything is moved to be get from DB (reports, orders...)
        }

        /// <summary>
        /// Gets food available for today
        /// </summary>
        /// <returns>List of meals</returns>
        public override List<Meal> GetDailyMenu()
        {
            var allFood = new List<Meal>();
            allFood.AddRange(DailyMenu());
            return allFood;
        }

        /// <summary>
        /// Get food from daily menu for today
        /// </summary>
        /// <returns>List of today available food</returns>
        private IEnumerable<Meal> DailyMenu()
        {
            var date = new DateTime(2020, 03, 03);
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

                for (int i = 0; i < foodNames.Count; i++)
                {
                    var foodName = foodNames.ElementAt(i).ToString();
                    var foodPrice = string.Empty;

                    if (i < foodPrices.Count)
                    {
                        foodPrice = foodPrices.ElementAt(i).ToString();
                    }

                    if (string.IsNullOrWhiteSpace(foodPrice))
                    {
                        // salad has the empty value because it is free of charge
                        foodPrice = "0";
                    }

                    if (!string.IsNullOrWhiteSpace(foodName) &&
                        decimal.TryParse(foodPrice, out decimal price))
                    {
                        result.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant });
                        if (i == foodNames.Count - 3 || i == foodNames.Count - 2)
                        {
                            // currently in Topli Obrok restaurant SALAD is always third and second from the end meal in daily sheet
                            result.ElementAt(i).Type = (int)MealType.SALAD;
                        }
                        else if (i == foodNames.Count - 1)
                        {
                            // currently in Topli Obrok restaurant SOUP is always the last meal in daily sheet
                            result.Last().Type = (int)MealType.SOUP;
                        }
                    }
                }

                return result;
            }
            else
            {
                return new List<Meal>();
            }
        }
    }
}
