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
    public sealed class LipaConnector : RestaurantConnector, ILipaConnector
    {
        public LipaConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, RestaurantConstants.POD_LIPOM_NAME)
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.LIPA);
            ColumnsPerDay = 10;
            DailyMenuDate = new DateTime(2020, 03, 03);
            DailyMenuSheet = GetLocalMonthName(DailyMenuDate.Month) + DailyMenuDate.Year;
        }

        /// <summary>
        /// Write menu with all meals in DB to sheet. Used for initial writing of old menu
        /// </summary>
        /// <param name="meals">List of all meals to be written</param>
        public override void WriteMenu(List<Meal> meals)
        {
            // not needed for now. But will probably be needed in the future to write orders
            // in sheets until everything is moved to be get from DB (reports, orders...)
        }

        /// <summary>
        /// Gets food available for today
        /// </summary>
        /// <returns>List of foods</returns>
        public List<Meal> GetDailyMenu()
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
            var date = new DateTime(2020, 03, 03); //DateTime.Today;
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
                    var foodPrice = foodPrices.ElementAt(i).ToString();

                    if (!string.IsNullOrWhiteSpace(foodName) &&
                        !string.IsNullOrWhiteSpace(foodPrice) &&
                        decimal.TryParse(foodPrice, out decimal price))
                    {
                        result.Add(new Meal { Name = foodName, Price = price, Restaurant = Restaurant });
                        if (i == foodNames.Count - 1)
                        {
                            // currently in Lipa restaurant SOUP is always the last meal in daily sheet
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
