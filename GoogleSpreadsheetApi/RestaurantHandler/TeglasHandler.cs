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
    public class TeglasHandler
    {
        public static string ordersSheet = "Narudzbine";
        public static string foodListSheet = "Cene i opis";

        Restaurant restaurant;
        SheetsService GoogleSS;
        private string sheetId;

        public TeglasHandler(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
        {
            GoogleSS = GoogleSSFactory.GetService();
            sheetId = GoogleSSIdFactory.GetNewTeglas();
            restaurant = new Restaurant { Name = "Teglas" };
        }

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
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = GoogleSS.Spreadsheets.Values.Clear(body, sheetId, ordersSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = GoogleSS.Spreadsheets.Values.Update(orderRange, sheetId, ordersSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            updateRequest.Execute();
        }
        
        public void WriteMenu(List<Food> foods)
        {
            List<object> header = new List<object> { "Naziv jela", "Opis", "Cena" };
            ValueRange foodRange = new ValueRange();
            foodRange.Values = new List<IList<object>>();
            foodRange.Values.Add(header);

            foreach (var food in foods)
            {
                List<object> foodData = new List<object>();
                foodData.Add(food.Name);
                foodData.Add(food.Description);
                foodData.Add(food.Price);
                foodRange.Values.Add(foodData);
            }

            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = GoogleSS.Spreadsheets.Values.Clear(body, sheetId, foodListSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = GoogleSS.Spreadsheets.Values.Update(foodRange, sheetId, foodListSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            updateRequest.Execute();


        }
    }
}
