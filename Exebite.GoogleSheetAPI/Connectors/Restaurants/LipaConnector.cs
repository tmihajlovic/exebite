using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Common;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public sealed class LipaConnector : RestaurantConnector, ILipaConnector
    {
        private readonly string _sheetId;
        private readonly Restaurant _restaurant;

        public LipaConnector(IGoogleSheetExtractor googleSheetService, IGoogleSpreadsheetIdFactory googleSSIdFactory)
            : base(googleSheetService)
        {
            _sheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.LIPA);
            _restaurant = new Restaurant { Name = "Restoran pod Lipom" };
            SheetId = _sheetId;
            DailyMenuSheet = GetLocalMonthName(DateTime.Now.Month) + DateTime.Now.Year;
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
            var foodRange = new ValueRange { Values = new List<IList<object>> { header } };

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
            // var date = DateTime.Today;
            var date = new DateTime(2020, 01, 06);
            var foundMerge = FindDateRangeInSheets(date);

            if (foundMerge.IsSuccess)
            {
                var namesRange = A1Notation.ToRangeFormat(
                        startColumn: foundMerge.Value.Range.StartColumnIndex.Value,
                        startRow: 0, // Start Corner
                        endColumn: foundMerge.Value.Range.EndColumnIndex.Value,
                        endRow: 4); // End corner

                var offersList = GoogleSheetService.ReadSheetData(string.Format("'{0}'!{1}", foundMerge.Value.SheetName, namesRange), _sheetId);

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
                        result.Add(new Food { Name = foodName, Price = price, Restaurant = _restaurant });
                    }
                }

                return result;
            }
            else
            {
                return new List<Food>();
            }
        }

        /// <summary>
        /// Returns a list of all merged regions
        /// </summary>
        /// <param name="sheet">Google sheet for which will be checked for merged regions.</param>
        /// <returns>Returns all merged regions from the sheet.</returns>
        private IEnumerable<MergedRegion> SheetMerges(Sheet sheet)
        {
            if (sheet.Merges == null)
            {
                return new List<MergedRegion>();
            }

            return sheet.Merges.Select(merge => new MergedRegion(sheet, merge));
        }

        /// <summary>
        /// Finds the merge inside all the sheets.
        /// </summary>
        /// <param name="providedDate">Date for which needs to be found date range</param>
        /// <returns>Result with all <seealso cref="MergedRegion"/></returns>
        private Result<MergedRegion> FindDateRangeInSheets(DateTime providedDate)
        {
            var foundRegion = GoogleSheetService.GetWorkSheets(_sheetId)
                .Select(SheetMerges)
                .Select(mergeList => GetMergeWithDate(mergeList, providedDate))
                .Where(result => result.IsSuccess)
                .FirstOrDefault();

            if (foundRegion != null)
            {
                return foundRegion;
            }

            return Result<MergedRegion>.Fail(null, string.Format("No merge with provided date {0}.", providedDate.ToString()));
        }

        /// <summary>
        /// From all the merges, find the one that has provided date.
        /// </summary>
        /// <param name="merges">Collection of all <seealso cref="MergedRegion"/></param>
        /// <param name="providedDate">Date for which <paramref name="merges"/> will be checked.</param>
        /// <returns>Result with <seealso cref="MergedRegion"/></returns>
        private Result<MergedRegion> GetMergeWithDate(IEnumerable<MergedRegion> merges, DateTime providedDate)
        {
            foreach (var merge in merges)
            {
                var result = GoogleSheetService.ReadDateTime(merge.A1FirstCell, _sheetId);
                if (result.IsSuccess)
                {
                    DateTime parsedDate = result.Value;

                    if (providedDate.Year != parsedDate.Year
                        || providedDate.Month != parsedDate.Month)
                    {
                        break;
                    }

                    if (providedDate.Date.Equals(parsedDate.Date))
                    {
                        return Result<MergedRegion>.Success(merge);
                    }
                }

                // There is a limitation on Google Side for 100 calls per second.
                // We have added this thread sleep to avoid such issues.
                Thread.Sleep(Constants.SLEEP_TIME);
            }

            return Result<MergedRegion>.Fail(null, "Sheet doesn't contain this date.");
        }
    }
}
