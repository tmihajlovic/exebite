using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using GoogleSpreadsheetApi.GoogleSSFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSpreadsheetApi.RestaurantHandler
{
    public class LipaHandler
    {
        public static string dailyMenuSheet = "Dnevni meni";
        public static string alwaysAvailableSheet = "Uvek dostupno";
        public static string foodListSheet = "Cene i opis";
        public static string ordersSheet = "Narudzbine";

        Restaurant restaurant;
        SheetsService GoogleSS;
        private string sheetId;

        public LipaHandler(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
        {
            GoogleSS = GoogleSSFactory.GetService();
            sheetId = GoogleSSIdFactory.GetNewLipa();
            restaurant = new Restaurant { Name = "Restoran pod Lipom" };
        }

        public void PlaceOrders(List<Order> orders)
        {
            List<object> header = new List<object> {"Jelo","Komada","Cena","Cena Ukupno","Narucili"};

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
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = GoogleSS.Spreadsheets.Values.Clear(body, sheetId, ordersSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = GoogleSS.Spreadsheets.Values.Update(orderRange , sheetId, ordersSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            
            updateRequest.Execute();

        }

        public List<Food> GetDalyMenu()
        {
            List<Food> allFood = new List<Food>();

            allFood.AddRange(DailyMenu());
            allFood.AddRange(AaMenu());

            return allFood;
        }

        private List<Food> DailyMenu()
        {
            List<Food> dailyFood = new List<Food>();

            var today = DateTime.Today.DayOfWeek;
            string todayDayColumn = "A";
            switch(today)
            {
                case DayOfWeek.Monday:
                    todayDayColumn = "A";
                    break;

                case DayOfWeek.Tuesday:
                    todayDayColumn = "B";
                    break;

                case DayOfWeek.Wednesday:
                    todayDayColumn = "C";
                    break;

                case DayOfWeek.Thursday:
                    todayDayColumn = "D";
                    break;

                case DayOfWeek.Friday:
                    todayDayColumn = "E";
                    break;
            }


            var range = dailyMenuSheet + "!" + todayDayColumn+"2:"+todayDayColumn+"1000";
            
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(sheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            dailyFood = sheetData.Values[0].Select(f => new Food { Name = f.ToString(), Restaurant = restaurant }).ToList();

            return dailyFood;
        }
        private List<Food>AaMenu()
        {
            List<Food> aaFood = new List<Food>();

            var range = alwaysAvailableSheet + "!A1:A1000";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(sheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            aaFood = sheetData.Values[0].Select(f => new Food { Name = f.ToString(), Restaurant = restaurant }).ToList();
            return aaFood;
        }
    }
}
