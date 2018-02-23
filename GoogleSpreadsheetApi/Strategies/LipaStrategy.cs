using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSpreadsheetApi.Strategies
{
    public class LipaStrategy : IRestaurantStrategy
    {
        Restaurant restaurant;
        SheetsService GoogleSS;
        private string LipaSpredSheet;

        public LipaStrategy(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
        {
            restaurant = new Restaurant { Name = "Restoran pod Lipom" };
            GoogleSS = GoogleSSFactory.GetService();
            LipaSpredSheet = GoogleSSIdFactory.GetLipa();
        }

        /// <summary>
        /// Gets active sheet in shredsheet
        /// </summary>
        /// <returns>Name of the sheet</returns>
        private string GetActiveSheet()
        {
            string SpreadSheetID = LipaSpredSheet;
            SpreadsheetsResource.GetRequest request =
                GoogleSS.Spreadsheets.Get(SpreadSheetID);
            request.Fields = "sheets(properties(hidden,index,sheetId,title))";
            var result = request.Execute();
            var sheet = result.Sheets.FirstOrDefault(s => s.Properties.Index != 0 && s.Properties.Hidden != true).Properties.Title;

            return sheet;
        }

        /// <summary>
        /// Returns last column thath contaain value in active sheet
        /// </summary>
        /// <returns>A1 notation of last column</returns>
        public string GetLastColumn()
        {
            var sheet = this.GetActiveSheet();
            string SpreadSheetRange = sheet;
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(LipaSpredSheet, SpreadSheetRange);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            request.Fields = "range";
            ValueRange response = request.Execute();
            string rangeEnd = response.Range.Substring(response.Range.IndexOf(':') + 1);
            var lastColumn = new String(rangeEnd.TakeWhile(Char.IsLetter).ToArray());

            return lastColumn;
        }

        /// <summary>
        /// Get menu for today
        /// </summary>
        /// <returns>List of foods for today</returns>
        public List<Food> GetDailyMenu()
        {
            var sheet = GetActiveSheet();
            var lastColumn = this.GetLastColumn();
            var range = sheet + "!C3:" + lastColumn + "7";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(LipaSpredSheet, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            List<Food> foodList = new List<Food>();
            string today = "06-Feb-2018";//DateTime.Today in productio
            string tomorrow = "07-Feb-2018"; // real in production!!!

            IEnumerable<IList<object>> todayData = null;
            
            todayData = sheetData.Values.SkipWhile(m => m[1].ToString() != today)
                .TakeWhile(n => n[1].ToString() != tomorrow);// get values for today
            foodList = todayData.Select(f => new Food { Name = f[0].ToString(), Price = decimal.Parse(f[3].ToString()) }).ToList();// make Food from values
            foodList = foodList.Where(f => f.Name != "").ToList();// remove empty cells

            return foodList;
        }

        /// <summary>
        /// Get all orders from current sheet
        /// </summary>
        /// <returns>List of orders</returns>
        public List<Order> GetHistoricalData()
        {
            var sheet = GetActiveSheet();
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(LipaSpredSheet, sheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();
            List<Order> orderList = new List<Order>();
            var orderData = sheetData.Values.Skip(2);
            var tmpDate = "";

            foreach(var order in orderData)
            {
                //update current date
                if(order[3].ToString() != "")
                {
                    tmpDate = order[3].ToString();
                }
                //if orders exist 
                if(order.Count > 7)
                {
                    //look for "x" cell, if exist make new order for user on that row
                    for (int i=8; i< order.Count; i++)
                    {
                        if(order[i].ToString() != "")
                        {
                            orderList.Add(new Order {
                                Price = decimal.Parse(order[5].ToString()),
                                Date = DateTime.Parse(tmpDate),
                                Customer = new Customer {
                                    Name = sheetData.Values[1][i].ToString()
                                },
                                Meal = new Meal {
                                    Foods = new List<Food>() {
                                         new Food
                                         {
                                             Name = order[2].ToString(),
                                             Price = decimal.Parse(order[5].ToString()),
                                             Type = FoodType.MAIN_COURSE,
                                             Restaurant = restaurant,
                                             IsInactive = false
                                         }
                                     },
                                    Price = decimal.Parse(order[5].ToString())
                                }
                            });
                        }
                    }
                }

            }
            return orderList;
        }

        public void PlaceOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
