using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSpreadsheetApi.Strategies
{
    public class HedoneStrategy : IRestaurantStrategy
    {
        private string HedoneSpredSheet;
        Restaurant restaurant;
        SheetsService GoogleSS;

        public HedoneStrategy(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
        {
            restaurant = new Restaurant { Name = "Hedone" };
            GoogleSS = GoogleSSFactory.GetService();
            HedoneSpredSheet = GoogleSSIdFactory.GetHedone();
        }
        
        /// <summary>
        /// Gets active sheet in shredsheet
        /// </summary>
        /// <returns>Name of the sheet</returns>
        private string GetActiveSheet()
        {
            string SpreadSheetID = HedoneSpredSheet;
            SpreadsheetsResource.GetRequest request =
                GoogleSS.Spreadsheets.Get(SpreadSheetID);
            request.Fields = "sheets(properties(hidden,index,sheetId,title))";
            var result = request.Execute();
            var sheet = result.Sheets.FirstOrDefault(s => s.Properties.Title.Any(c => char.IsDigit(c)) && s.Properties.Hidden != true).Properties.Title;

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
                        GoogleSS.Spreadsheets.Values.Get(HedoneSpredSheet, SpreadSheetRange);
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
        /// <returns>List of Food available</returns>
        public List<Food> GetDailyMenu()
        {
            List<Food> foodList = new List<Food>();
            //get daily menu
            var sheet = GetActiveSheet();
            var lastColumn = this.GetLastColumn();
            var range = sheet + "!C3:" + lastColumn + "7";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(HedoneSpredSheet, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            string today = "Tue, February 6, ";//DateTime.Today in productio
            string tomorrow = "Wed, February 7, "; // real in production!!!

            IEnumerable<IList<object>> foodData = null;

            foodData = sheetData.Values.SkipWhile(m => m[1].ToString() != today)
                .TakeWhile(n => n[1].ToString() != tomorrow);// get values for today
            foodList = foodData.Select(f => new Food { Name = f[0].ToString(), Price = decimal.Parse(f[3].ToString()) }).ToList();// make Food from values
            foodList = foodList.Where(f => f.Name != "").ToList();// remove empty cells

            //get always available menu

            sheet = "Opis jela sa cenama";
            range = sheet + "!A3:D999";
            request = GoogleSS.Spreadsheets.Values.Get(HedoneSpredSheet, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            sheetData = request.Execute();
            foodData = sheetData.Values.Where(f => f.Count != 0);
            foreach(var item in foodData)
            {
                foodList.Add(new Food {
                    Name = item[0].ToString(),
                    Type = FoodType.MAIN_COURSE,
                    Price = decimal.Parse(item[3].ToString()),
                    Description = item[1].ToString(),
                    IsInactive = false
                });
            }

            return foodList;
        }

        /// <summary>
        /// Get historical data
        /// </summary>
        /// <returns>List of oredrs</returns>
        public List<Order> GetHistoricalData()
        {
            //daily
            var sheet = GetActiveSheet();
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(HedoneSpredSheet, sheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();
            List<Order> orderList = new List<Order>();
            var orderData = sheetData.Values.Skip(2);
            var tmpDate = "";

            foreach (var order in orderData)
            {
                //update current date
                if (order[3].ToString() != "")
                {
                    tmpDate = order[3].ToString();
                }
                //if orders exist 
                if (order.Count > 8)
                {
                    //look for "x" cell, if exist make new order for user on that row
                    for (int i = 8; i < order.Count-1; i++)
                    {
                        if (order[i].ToString() != "")
                        {
                            orderList.Add(new Order
                            {
                                Price = decimal.Parse(order[5].ToString()),
                                Date = DateTime.Parse(tmpDate),
                                Customer = new Customer
                                {
                                    Name = sheetData.Values[1][i].ToString()
                                },
                                Meal = new Meal
                                {
                                    Foods = new List<Food>() {
                                         new Food
                                         {
                                             Name = order[2].ToString(),
                                             Price = decimal.Parse(order[5].ToString()),
                                             Type = FoodType.MAIN_COURSE,
                                             Restaurant = restaurant
                                         }
                                     },
                                    Price = decimal.Parse(order[5].ToString())
                                }
                            });
                        }
                    }
                }

            }
            //always aviable 
            //get food info
            List<Food> foodList = new List<Food>();
            sheet = "Opis jela sa cenama";
            var range = sheet + "!A3:D999";
            request = GoogleSS.Spreadsheets.Values.Get(HedoneSpredSheet, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            sheetData = request.Execute();
            var foodData = sheetData.Values.Where(f => f.Count != 0);
            foreach (var item in foodData)
            {
                foodList.Add(new Food
                {
                    Name = item[0].ToString(),
                    Type = FoodType.MAIN_COURSE,
                    Price = decimal.Parse(item[3].ToString()),
                    Restaurant = restaurant
                });
            }

            //get orders
            sheet = "Jela po porudžbini";
            request =
                        GoogleSS.Spreadsheets.Values.Get(HedoneSpredSheet, sheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            sheetData = request.Execute();
            orderData = sheetData.Values.Skip(1);

            foreach (var order in orderData)
            {
                if (order.Count > 1)
                {
                    for (int i = 1; i < order.Count - 1; i++)
                    {
                        if (order[i].ToString() != "")
                        {
                            orderList.Add(new Order
                            {
                                Meal = new Meal
                                {
                                    Foods = new List<Food>
                                    {
                                        foodList.SingleOrDefault(f => f.Name == order[i].ToString())
                                    },
                                    Price = foodList.SingleOrDefault(f => f.Name == order[i].ToString()).Price
                                },
                                Price = foodList.SingleOrDefault(f => f.Name == order[i].ToString()).Price,
                                Date = DateTime.Parse(new String((order[0].ToString().SkipWhile(c => !char.IsNumber(c)).TakeWhile(c => c !=')')).ToArray())),
                                Customer = new Customer
                                {
                                    Name = sheetData.Values[0][i].ToString()
                                }

                            });
                        }
                    }
                }
            }

            return orderList;
            
        }

        public void PlaceOrders(List<Order> order)
        {
            throw new NotImplementedException();
        }
    }
}
