using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exebite.GoogleSpreadsheetApi.RestaurantConectors
{
    public abstract class RestaurantConector : IRestaurantConector
    {
        internal SheetsService _GoogleSS;
        internal string _sheetId;
        internal string _ordersSheet;
        internal string _dailyMenuSheet;

        public RestaurantConector()
        {
        }

        public abstract void WriteMenu(List<Food> foods);

        public abstract List<Food> GetDalyMenu();

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

            foreach (var food in distinctFood)
            {
                List<object> customerList = new List<object>();
                List<object> formatedData = new List<object>();

                foreach (var order in orders)
                {
                    if (order.Meal.Foods.FirstOrDefault(f => f.Name == food.Name) != null)
                    {
                        customerList.Add(order.Customer.Name);
                    }
                }

                formatedData.Add(food.Name);
                formatedData.Add(customerList.Count());
                formatedData.Add(food.Price);
                formatedData.Add(food.Price * customerList.Count());
                formatedData.AddRange(customerList);
                orderRange.Values.Add(formatedData);
            }


            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = _GoogleSS.Spreadsheets.Values.Clear(body, _sheetId, _ordersSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = _GoogleSS.Spreadsheets.Values.Update(orderRange, _sheetId, _ordersSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            updateRequest.Execute();

        }


        public void DnevniMenuSheetSetup()
        {

            SpreadsheetsResource.ValuesResource.GetRequest request =
                        _GoogleSS.Spreadsheets.Values.Get(_sheetId, _dailyMenuSheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();


            var sheetValues = sheetData.Values;
            var dayOfWeek = this.GetLocalDayName(DayOfWeek.Thursday);
            int today = 0;
            for (int i = 0; i < sheetValues.Count; i++)
            {
                if (sheetValues[i][0].ToString() == dayOfWeek)
                    today = i;
            }

            ValueRange updatedRange = new ValueRange();
            updatedRange.Values = new List<IList<object>>();

            //insert today and after
            for (int i = today; i < sheetValues.Count; i++)
            {
                updatedRange.Values.Add(sheetValues[i]);
            }
            //insert before today
            for (int k = 0; k < today; k++)
            {
                updatedRange.Values.Add(sheetValues[k]);
            }

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
