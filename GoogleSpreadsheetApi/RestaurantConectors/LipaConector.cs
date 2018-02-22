using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exebite.GoogleSpreadsheetApi.RestaurantConectors
{
    public class LipaConector : RestaurantConector, ILipaConector
    {
        public static string dailyMenuSheet = "Dnevni meni";
        public static string foodListSheet = "Cene i opis";
        public static string ordersSheet = "Narudzbine";

        Restaurant restaurant;
        SheetsService GoogleSS;
        private string sheetId;

        public LipaConector(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
        {
            GoogleSS = GoogleSSFactory.GetService();
            sheetId = GoogleSSIdFactory.GetNewLipa();
            restaurant = new Restaurant { Name = "Restoran pod Lipom" };
            _sheetId = sheetId;
            _ordersSheet = ordersSheet;
            _GoogleSS = GoogleSS;
            _dailyMenuSheet = dailyMenuSheet;
            _foodListSheet = foodListSheet;
            _restaurant = restaurant;
        }
        
        

        public override void WriteMenu(List<Food> foods)
        {
            List<object> header = new List<object> { "Naziv jela", "Opis", "Cena", "Tip" };
            ValueRange foodRange = new ValueRange();
            foodRange.Values = new List<IList<object>>();
            foodRange.Values.Add(header);

            foreach( var food in foods)
            {
                List<object> foodData = new List<object>();
                foodData.Add(food.Name);
                foodData.Add(food.Description);
                foodData.Add(food.Price);
                foodData.Add(GetLocalFoodType(food.Type));
                foodRange.Values.Add(foodData);
            }

            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = GoogleSS.Spreadsheets.Values.Clear(body, sheetId, foodListSheet);
            clearRequest.Execute();

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = GoogleSS.Spreadsheets.Values.Update(foodRange, sheetId, foodListSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            updateRequest.Execute();

        }

        public override List<Food> GetDalyMenu()
        {
            List<Food> allFood = new List<Food>();

            allFood.AddRange(DailyMenu());

            return allFood;
        }
        

        private IEnumerable<Food> DailyMenu()
        {
            IEnumerable<Food> dailyFood = new List<Food>();

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
        
    }
}
