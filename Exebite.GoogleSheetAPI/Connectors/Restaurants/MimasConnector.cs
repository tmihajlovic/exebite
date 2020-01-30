using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Common;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public sealed class MimasConnector : RestaurantConnector, IMimasConnector
    {
        public MimasConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, "Mimas")
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.MIMAS);
            DailyMenuSheet = GetLocalMonthName(DateTime.Now.Month) + DateTime.Now.Year;
        }

        /// <summary>
        /// Write menu with all foods in DB to sheet. Used for initial writing of old menu
        /// </summary>
        /// <param name="foods">List of all food to be written</param>
        public override void WriteMenu(List<Food> foods)
        {
            // not needed for now. But will probably be needed in the future to write orders
            // in sheets until everything is moved to be get from DB (reports, orders...)
        }

        /// <summary>
        /// Gets food available for today
        /// </summary>
        /// <returns>List of foods</returns>
        public override List<Food> GetDailyMenu()
        {
            var allFood = new List<Food>();
            allFood.AddRange(DailyMenu());
            return allFood;
        }

        /// <summary>
        /// Get food from daily menu for today
        /// </summary>
        /// <returns>List of today available food</returns>
        private IEnumerable<Food> DailyMenu()
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

                var result = new List<Food>();
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
                        result.Add(new Food { Name = foodName, Price = price, RestaurantId = Restaurant.Id });
                    }
                }

                return result;
            }
            else
            {
                return new List<Food>();
            }
        }
    }
}
