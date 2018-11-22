using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public abstract class RestaurantConector : IRestaurantConector
    {
        private readonly string _kasaSheet = "Kasa";

        protected RestaurantConector(IGoogleSheetService googleSheetService)
        {
            GoogleSheetService = googleSheetService;
        }

        internal IGoogleSheetService GoogleSheetService { get; set; }

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

            bool empty = true;
            int rowNum = 0;
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
        /// Populate Kasa tab
        /// </summary>
        /// <param name="customerList">List of <see cref="Customer"/></param>
        public void WriteKasaTab(List<Customer> customerList)
        {
            if (customerList == null)
            {
                return;
            }

            List<object> header = new List<object> { "Id", "Ime i prezime", "Suma" };
            ValueRange kasaData = new ValueRange { Values = new List<IList<object>> { header } };

            foreach (var customer in customerList)
            {
                kasaData.Values.Add(new List<object>
                {
                    customer.Id,
                    customer.Name,
                    customer.Orders.Where(o => o.Meal.Foods[0].Restaurant.Name == Restaurant.Name).Sum(o => o.Price)
                });
            }

            GoogleSheetService.Clear(SheetId, _kasaSheet);
            GoogleSheetService.Update(kasaData, SheetId, _kasaSheet);
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
    }
}
