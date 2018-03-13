using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exebite.GoogleSheetAPI.RestaurantConectors
{
    public abstract class RestaurantConector : IRestaurantConector
    {
        internal SheetsService _GoogleSS;
        internal string _sheetId;
        internal string _ordersSheet;
        internal string _dailyMenuSheet;
        internal string _foodListSheet;
        internal Restaurant _restaurant;
        internal string _kasaSheet = "Kasa";

        public RestaurantConector()
        {
        }

        public abstract void WriteMenu(List<Food> foods);

        public abstract List<Food> GetDailyMenu();

        /// <summary>
        /// Populate Orders tab in sheet with new order data
        /// </summary>
        /// <param name="orders">List of orders to write</param>
        public void PlaceOrders(List<Order> orders)
        {

            List<object> header = new List<object> { "Jelo", "Komada", "Cena", "Cena Ukupno", "Narucili" };

            List<Food> listOFOrderdFood = new List<Food>();
            foreach (var order in orders)
            {
                foreach (var food in order.Meal.Foods)
                {
                    listOFOrderdFood.Add(food);
                }
            }
            var distinctFood = listOFOrderdFood.GroupBy(f => f.Id).Select(o => o.FirstOrDefault());
            ValueRange orderRange = new ValueRange();
            orderRange.Values = new List<IList<object>>();
            orderRange.Values.Add(header);
            int rowCounter = 2;// First row with orders, used for formula
            foreach (var food in distinctFood)
            {
                List<object> customerList = new List<object>();
                List<object> formatedData = new List<object>();

                foreach (var order in orders)
                {
                    if (order.Meal.Foods.FirstOrDefault(f => f.Name == food.Name) != null)
                    {
                        if (order.Note != null && order.Note != "")
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
                formatedData.Add(customerList.Count());
                formatedData.Add(food.Price);
                formatedData.Add("=" + "B" +rowCounter+ "*" +"C"+rowCounter);//add formula to sum
                rowCounter++;
                formatedData.AddRange(customerList);
                orderRange.Values.Add(formatedData);
            }


            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = _GoogleSS.Spreadsheets.Values.Clear(body, _sheetId, _ordersSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = _GoogleSS.Spreadsheets.Values.Update(orderRange, _sheetId, _ordersSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            updateRequest.Execute();

        }
        /// <summary>
        /// Setup daily menu sheet, making today first column
        /// </summary>
        public void DnevniMenuSheetSetup()
        {
            //get data
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        _GoogleSS.Spreadsheets.Values.Get(_sheetId, _dailyMenuSheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            DateTime dateCounter = DateTime.Today;

            var sheetValues = sheetData.Values;
            var dayOfWeek = this.GetLocalDayName(dateCounter.DayOfWeek);
            int today = 0;
            for (int i = 0; i < sheetValues.Count; i++)
            {
                if (sheetValues[i][0].ToString() == dayOfWeek)
                    today = i;
            }

            ValueRange updatedRange = new ValueRange();
            updatedRange.Values = new List<IList<object>>();
            int daysToAdd = 0;
            // insert today and after
            for (int i = today; i < sheetValues.Count; i++)
            {
                sheetValues[i][1] =dateCounter.AddDays(daysToAdd).ToString("dd-MM-yyyy");
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
            // insert before today
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
            ValueRange formatedRange = new ValueRange();
            formatedRange.Values = new List<IList<object>>();

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
                        row.Add("");
                    }
                }
                if (!empty)
                {
                    formatedRange.Values.Add(row);
                    rowNum++;
                }
            } while (!empty);



            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = _GoogleSS.Spreadsheets.Values.Clear(body, _sheetId, _dailyMenuSheet);
            clearRequest.Execute();


            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = _GoogleSS.Spreadsheets.Values.Update(formatedRange, _sheetId, _dailyMenuSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            updateRequest.Execute();

        }

        /// <summary>
        /// Loads values from sheet, with all info
        /// </summary>
        /// <returns>List of all food from sheet</returns>
        public List<Food> LoadAllFoods()
        {
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        _GoogleSS.Spreadsheets.Values.Get(_sheetId, _foodListSheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            ValueRange sheetData = request.Execute();

            var foodData = sheetData.Values.Skip(1);

            var foods = foodData.Select(item => new Food(){
                Name = item[0].ToString(),
                Description = item[1].ToString(),
                Price = decimal.Parse(item[2].ToString()),
                Type = GetFoodType(item[3].ToString()),
                Restaurant = _restaurant,
                IsInactive = false
            });

            return foods.ToList();
        }

        /// <summary>
        /// Populate "Kasa" tab 
        /// </summary>
        public void WriteKasaTab(List<Customer> customerList)
        {
            List<object> header = new List<object> { "Id", "Ime i prezime", "Suma"};
            ValueRange kasaData = new ValueRange();
            kasaData.Values = new List<IList<object>>();
            kasaData.Values.Add(header);

            foreach(var customer in customerList)
            {
                var row = new List<object>();
                row.Add(customer.Id);
                row.Add(customer.Name);
                row.Add(customer.Orders.Where(o => o.Meal.Foods[0].Restaurant.Name == _restaurant.Name).Sum(o => o.Price));
                kasaData.Values.Add(row);
            }



            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = _GoogleSS.Spreadsheets.Values.Clear(body, _sheetId, _kasaSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = _GoogleSS.Spreadsheets.Values.Update(kasaData, _sheetId, _kasaSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            updateRequest.Execute();

        }
        /// <summary>
        /// Translate name of day
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns>Translated day of week</returns>
        internal string GetLocalDayName(DayOfWeek day)
        {
            var dayString = "";
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
            }
            return dayString;
        }
        /// <summary>
        /// Translate food type 
        /// </summary>
        /// <param name="foodType">Value of food enum</param>
        /// <returns>Name</returns>
        internal string GetLocalFoodType(FoodType foodType)
        {
            string typeLocal = "Glavno jelo";

            switch(foodType)
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
            }

            return typeLocal;
        }
        /// <summary>
        /// Translate food type to enum value
        /// </summary>
        /// <param name="type">Type of food</param>
        /// <returns>Enum value</returns>
        internal FoodType GetFoodType(string type)
        {
            FoodType result = FoodType.MAIN_COURSE;

            switch(type)
            {
                case "Glavno jelo":
                    result = FoodType.MAIN_COURSE;
                    break;

                case "Prilog":
                    result = FoodType.SIDE_DISH;
                    break;

                case "Salata":
                    result = FoodType.SALAD;
                    break;

                case "Desert":
                    result = FoodType.DESERT;
                    break;

                case "Supa":
                    result = FoodType.SOUP;
                    break;

                case "Dodatak":
                    result = FoodType.CONDIMENTS;
                    break;
            }
            return result;
        }
    }
}
