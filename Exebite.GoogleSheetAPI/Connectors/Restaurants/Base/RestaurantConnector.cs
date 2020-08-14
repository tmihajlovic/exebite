using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Common;
using Exebite.GoogleSheetAPI.SheetExtractor;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants.Base
{
    public abstract class RestaurantConnector : IRestaurantConnector
    {
        private const string _orderMark = "x";

        private readonly IRestaurantQueryRepository _restaurantQueryRepository;

        protected RestaurantConnector(
            IGoogleSheetExtractor googleSheetService,
            IRestaurantQueryRepository restaurantQueryRepository,
            string restaurantName)
        {
            _restaurantQueryRepository = restaurantQueryRepository;
            GoogleSheetService = googleSheetService;
            Restaurant = GetRestaurant(restaurantName);
            DailyMenuDate = DateTime.ParseExact("2020-02-03", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        internal IGoogleSheetExtractor GoogleSheetService { get; set; }

        internal string SheetId { get; set; }

        internal string OrdersSheet { get; set; }

        internal string DailyMenuSheet { get; set; }

        internal int ColumnsPerDay { get; set; }

        internal DateTime DailyMenuDate { get; }

        internal string FoodListSheet { get; set; }

        internal Restaurant Restaurant { get; set; }

        public abstract void WriteMenu(List<Meal> foods);

        public void WriteOrder(string customerName, string locationName, ICollection<Meal> meals)
        {
            var valueRange = GoogleSheetService.GetRows(SheetId, DailyMenuSheet);

            int startDateIndex = GetStartDateColumnIndex(valueRange);
            int endDateIndex = startDateIndex + ColumnsPerDay;

            object[] mealsData = new object[ColumnsPerDay];

            foreach (var meal in meals)
            {
                for (int mealIndex = startDateIndex; mealIndex < endDateIndex; mealIndex++)
                {
                    mealsData[mealIndex - startDateIndex] = valueRange.Values[0][mealIndex].ToString() == meal.Name ? _orderMark : string.Empty;
                }
            }

            var mealsBody = new ValueRange()
            {
                Values = new List<IList<object>>() { mealsData }
            };

            int rowIndex = GetCustomerRowIndex(valueRange, customerName);

            GoogleSheetService.Update(
                mealsBody,
                SheetId,
                DailyMenuSheet + "!" + A1Notation.ToCellFormat(startDateIndex, rowIndex) + ":" + A1Notation.ToCellFormat(endDateIndex, rowIndex));

            var locationBody = new ValueRange()
            {
                Values = new List<IList<object>>() { new object[1] { locationName.ToUpper() } }
            };

            GoogleSheetService.Update(
                locationBody,
                SheetId,
                DailyMenuSheet + "!" + A1Notation.ToCellFormat(2, rowIndex));
        }

        public int WorkingDayInMonth(int year, int month, int day)
        {
            int days = DateTime.DaysInMonth(year, month);
            List<DateTime> dates = new List<DateTime>();
            for (int i = 1; i <= days; i++)
            {
                dates.Add(new DateTime(year, month, i));
            }

            dates = dates.Where(d => d.DayOfWeek > DayOfWeek.Sunday & d.DayOfWeek < DayOfWeek.Saturday).ToList();
            for (int i = 0; i < dates.Count; i++)
            {
                if (dates[i].Day == day)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Translate name of day
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns>Translated day of week</returns>
        internal static string GetLocalDayName(DayOfWeek day)
        {
            var dayString = string.Empty;
            switch (day)
            {
                case DayOfWeek.Monday:
                    dayString = "Ponedeljak";
                    break;

                case DayOfWeek.Tuesday:
                    dayString = "Utorak";
                    break;

                case DayOfWeek.Wednesday:
                    dayString = "Sreda";
                    break;

                case DayOfWeek.Thursday:
                    dayString = "Cetvrtak";
                    break;

                case DayOfWeek.Friday:
                    dayString = "Petak";
                    break;
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                default:
                    break;
            }

            return dayString;
        }

        /// <summary>
        /// Translate food type
        /// </summary>
        /// <param name="foodType">Value of food enum</param>
        /// <returns>Name</returns>
        internal static string GetLocalFoodType(MealType foodType)
        {
            string typeLocal = "Glavno jelo";

            switch (foodType)
            {
                case MealType.MAIN_COURSE:
                    typeLocal = "Glavno jelo";
                    break;

                case MealType.SIDE_DISH:
                    typeLocal = "Prilog";
                    break;

                case MealType.SALAD:
                    typeLocal = "Salata";
                    break;

                case MealType.DESSERT:
                    typeLocal = "Desert";
                    break;

                case MealType.SOUP:
                    typeLocal = "Supa";
                    break;

                case MealType.CONDIMENT:
                    typeLocal = "Dodatak";
                    break;
                default:
                    break;
            }

            return typeLocal;
        }

        /// <summary>
        /// Translate food type to enum value
        /// </summary>
        /// <param name="type">Type of food</param>
        /// <returns>Enum value</returns>
        internal static MealType GetFoodType(string type)
        {
            if (type == null)
            {
                type = "Glavno jelo";
            }

            switch (type)
            {
                case "Glavno jelo":
                    return MealType.MAIN_COURSE;

                case "Prilog":
                    return MealType.SIDE_DISH;

                case "Salata":
                    return MealType.SALAD;

                case "Desert":
                    return MealType.DESSERT;

                case "Supa":
                    return MealType.SOUP;

                case "Dodatak":
                    return MealType.CONDIMENT;
            }

            return MealType.MAIN_COURSE;
        }

        /// <summary>
        /// Translate month number into the local month name.
        /// </summary>
        /// <param name="month">Month number</param>
        /// <returns>If correct month number is sent returns local month name, otherwise returns 'Not existing month'.</returns>
        internal static string GetLocalMonthName(int month)
        {
            var monthLocal = "Not existing month";

            switch (month)
            {
                case 1:
                    monthLocal = "Januar";
                    break;
                case 2:
                    monthLocal = "Februar";
                    break;
                case 3:
                    monthLocal = "Mart";
                    break;
                case 4:
                    monthLocal = "April";
                    break;
                case 5:
                    monthLocal = "Maj";
                    break;
                case 6:
                    monthLocal = "Jun";
                    break;
                case 7:
                    monthLocal = "Jul";
                    break;
                case 8:
                    monthLocal = "Avgust";
                    break;
                case 9:
                    monthLocal = "Septembar";
                    break;
                case 10:
                    monthLocal = "Oktobar";
                    break;
                case 11:
                    monthLocal = "Novembar";
                    break;
                case 12:
                    monthLocal = "Decembar";
                    break;
                default:
                    break;
            }

            return monthLocal;
        }

        /// <summary>
        /// Finds the merge inside all the sheets.
        /// </summary>
        /// <param name="providedDate">Date for which needs to be found date range</param>
        /// <returns>Result with all <seealso cref="MergedRegion"/></returns>
        internal Result<MergedRegion> FindDateRangeInSheets(
            DateTime providedDate)
        {
            var foundRegion = GoogleSheetService.GetWorkSheets(SheetId)
                .Select(SheetMerges)
                .Select(mergeList => GetMergeWithDate(mergeList, providedDate))
                .Where(result => result.IsSuccess)
                .LastOrDefault();

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
        internal Result<MergedRegion> GetMergeWithDate(
            IEnumerable<MergedRegion> merges,
            DateTime providedDate)
        {
            foreach (var merge in merges)
            {
                var result = GoogleSheetService.ReadDateTime(merge.A1FirstCell, SheetId);
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

            return sheet.Merges.Select(merge => new MergedRegion(sheet, merge))
                .Where(merge => merge.Range.StartRowIndex == 1 && merge.Range.EndColumnIndex - merge.Range.StartColumnIndex > 2);
        }

        /// <summary>
        /// Get the restaurant from the database
        /// </summary>
        /// <param name="name">Name of the restaurant that needs to be returned from database.</param>
        /// <returns>Returns restaurant object from database if exists, otherwise null.</returns>
        private Restaurant GetRestaurant(string name)
        {
            return _restaurantQueryRepository
                    .Query(new RestaurantQueryModel { Name = name })
                    .Map(res => res.Items.FirstOrDefault())
                    .Reduce(r => null, ex => Console.WriteLine(ex.ToString()));
        }

        private int GetCustomerRowIndex(ValueRange valueRange, string customerName)
        {
            for (int rowIndex = 6; rowIndex < valueRange.Values.Count; rowIndex++)
            {
                if (valueRange.Values[rowIndex][1].ToString() == customerName)
                {
                    return rowIndex;
                }
            }

            throw new Exception("Customer not found!");
        }

        private int GetStartDateColumnIndex(ValueRange valueRange)
        {
            for (int colIndex = 3; colIndex < valueRange.Values[1].Count; colIndex += ColumnsPerDay)
            {
                if (DateTime.ParseExact(valueRange.Values[1][colIndex].ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture).Date == DailyMenuDate.Date)
                {
                    return colIndex;
                }
            }

            throw new Exception("Date not found!");
        }
    }
}
