using System;
using System.Collections.Generic;
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
        private readonly string _kreditTab = "Kredit";
        private readonly IRestaurantQueryRepository _restaurantQueryRepository;

        protected RestaurantConnector(
            IGoogleSheetExtractor googleSheetService,
            IRestaurantQueryRepository restaurantQueryRepository,
            string restaurantName)
        {
            _restaurantQueryRepository = restaurantQueryRepository;
            GoogleSheetService = googleSheetService;
            Restaurant = GetRestaurant(restaurantName);
        }

        internal IGoogleSheetExtractor GoogleSheetService { get; set; }

        internal string SheetId { get; set; }

        internal string OrdersSheet { get; set; }

        internal string DailyMenuSheet { get; set; }

        internal string FoodListSheet { get; set; }

        internal Restaurant Restaurant { get; set; }

        public abstract void WriteMenu(List<Food> foods);

        public abstract List<Food> GetDailyMenu();

        /// <summary>
        /// Populate Orders tab in sheet with new order data
        /// </summary>
        /// <param name="orders">List of orders to write</param>
        public void PlaceOrders(List<Order> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }

            var header = new List<object> { "Jelo", "Komada", "Cena", "Cena Ukupno", "Narucili" };
            var orderRange = new ValueRange { Values = new List<IList<object>> { header } };

            List<Food> listOFOrderdFood = new List<Food>();
            foreach (var order in orders)
            {
                foreach (var food in order.Meal.Foods)
                {
                    listOFOrderdFood.Add(food);
                }
            }

            var distinctFood = listOFOrderdFood.GroupBy(f => f.Id).Select(o => o.FirstOrDefault());
            int rowCounter = 2; // First row with orders, used for formula
            foreach (var food in distinctFood)
            {
                var customerList = new List<object>();
                var formatedData = new List<object>();

                foreach (var order in orders)
                {
                    if (order.Meal.Foods.FirstOrDefault(f => f.Name == food.Name) != null)
                    {
                        if (!string.IsNullOrEmpty(order.Note))
                        {
                            customerList.Add(order.Customer.Name + "(" + order.Note + ")");
                        }
                        else
                        {
                            customerList.Add(order.Customer.Name);
                        }
                    }
                }

                formatedData.Add(food.Name);
                formatedData.Add(customerList.Count);
                formatedData.Add(food.Price);
                formatedData.Add($"=B{rowCounter}*C{rowCounter}"); // Add formula to sum
                rowCounter++;
                formatedData.AddRange(customerList);
                orderRange.Values.Add(formatedData);
            }

            GoogleSheetService.Clear(SheetId, OrdersSheet);
            GoogleSheetService.Update(orderRange, SheetId, OrdersSheet);
        }

        /// <summary>
        /// Setup daily menu sheet, making today first column
        /// </summary>
        public void DnevniMenuSheetSetup()
        {
            // Get data
            ValueRange sheetData = GoogleSheetService.GetColumns(SheetId, DailyMenuSheet);

            DateTime dateCounter = DateTime.Today;

            var sheetValues = sheetData.Values;
            var dayOfWeek = GetLocalDayName(dateCounter.DayOfWeek);
            int today = 0;
            for (int i = 0; i < sheetValues.Count; i++)
            {
                if (sheetValues[i][0].ToString() == dayOfWeek)
                {
                    today = i;
                }
            }

            ValueRange updatedRange = new ValueRange { Values = new List<IList<object>>() };
            const int daysToAdd = 0;

            // Insert today and after
            for (int i = today; i < sheetValues.Count; i++)
            {
                sheetValues[i][1] = dateCounter.AddDays(daysToAdd).ToString("dd-MM-yyyy");
                updatedRange.Values.Add(sheetValues[i]);
                if (dateCounter.DayOfWeek == DayOfWeek.Friday)
                {
                    dateCounter = dateCounter.AddDays(3);
                }
                else
                {
                    dateCounter = dateCounter.AddDays(1);
                }
            }

            // Insert before today
            for (int k = 0; k < today; k++)
            {
                sheetValues[k][1] = dateCounter.AddDays(daysToAdd).ToString("dd-MM-yyyy");
                updatedRange.Values.Add(sheetValues[k]);
                if (dateCounter.DayOfWeek == DayOfWeek.Friday)
                {
                    dateCounter = dateCounter.AddDays(3);
                }
                else
                {
                    dateCounter = dateCounter.AddDays(1);
                }
            }

            // Transpose values
            ValueRange formatedRange = new ValueRange { Values = new List<IList<object>>() };

            int rowNum = 0;
            bool empty;
            do
            {
                empty = true;
                List<object> row = new List<object>();
                for (int i = 0; i < updatedRange.Values.Count; i++)
                {
                    if (updatedRange.Values[i].Count > rowNum)
                    {
                        row.Add(updatedRange.Values[i][rowNum].ToString());
                        empty = false;
                    }
                    else
                    {
                        row.Add(string.Empty);
                    }
                }

                if (!empty)
                {
                    formatedRange.Values.Add(row);
                    rowNum++;
                }
            }
            while (!empty);

            GoogleSheetService.Clear(SheetId, DailyMenuSheet);
            GoogleSheetService.Update(formatedRange, SheetId, DailyMenuSheet);
        }

        /// <summary>
        /// Loads values from sheet, with all info
        /// </summary>
        /// <returns>List of all food from sheet</returns>
        public List<Food> LoadAllFoods()
        {
            ValueRange sheetData = GoogleSheetService.GetRows(SheetId, FoodListSheet);
            IEnumerable<Food> foods = new List<Food>();

            // Null and empty check
            if (!(sheetData?.Values?.Any() == true))
            {
                return foods.ToList();
            }

            var foodData = sheetData.Values.Skip(1);

            foods = foodData.Select(item => new Food()
            {
                Name = item[0].ToString(),
                Description = item[1].ToString(),
                Price = decimal.Parse(item[2].ToString()),
                Type = GetFoodType(item[3].ToString()),
                Restaurant = Restaurant,
                IsInactive = false
            });

            return foods.ToList();
        }

        /// <summary>
        /// Populate Kredit tab
        /// </summary>
        /// <param name="customerList">List of <see cref="Customer"/></param>
        [Obsolete("In Kredit tab and kasa should not be written")]
        public void WriteKasaTab(List<Customer> customerList)
        {
            if (customerList == null)
            {
                return;
            }

            List<object> header = new List<object> { "Id", "Ime i prezime", "SUM" };
            ValueRange kreditData = new ValueRange { Values = new List<IList<object>> { header } };

            foreach (var customer in customerList)
            {
                kreditData.Values.Add(new List<object>
                {
                    customer.Id,
                    customer.Name,
                    customer.Orders.Where(o => o.Meal.Foods[0].Restaurant.Name == Restaurant.Name).Sum(o => o.Price)
                });
            }

            GoogleSheetService.Clear(SheetId, _kreditTab);
            GoogleSheetService.Update(kreditData, SheetId, _kreditTab);
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
        internal static string GetLocalFoodType(FoodType foodType)
        {
            string typeLocal = "Glavno jelo";

            switch (foodType)
            {
                case FoodType.MAIN_COURSE:
                    typeLocal = "Glavno jelo";
                    break;

                case FoodType.SIDE_DISH:
                    typeLocal = "Prilog";
                    break;

                case FoodType.SALAD:
                    typeLocal = "Salata";
                    break;

                case FoodType.DESERT:
                    typeLocal = "Desert";
                    break;

                case FoodType.SOUP:
                    typeLocal = "Supa";
                    break;

                case FoodType.CONDIMENTS:
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
        internal static FoodType GetFoodType(string type)
        {
            if (type == null)
            {
                type = "Glavno jelo";
            }

            switch (type)
            {
                case "Glavno jelo":
                    return FoodType.MAIN_COURSE;

                case "Prilog":
                    return FoodType.SIDE_DISH;

                case "Salata":
                    return FoodType.SALAD;

                case "Desert":
                    return FoodType.DESERT;

                case "Supa":
                    return FoodType.SOUP;

                case "Dodatak":
                    return FoodType.CONDIMENTS;
            }

            return FoodType.MAIN_COURSE;
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

            return sheet.Merges.Select(merge => new MergedRegion(sheet, merge));
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
    }
}
