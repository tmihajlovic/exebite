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
            restaurant = new Restaurant { Name = "Restoran pod Lipom", Id = 1 };
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

            foreach (var order in orderData)
            {
                //update current date
                if (order[3].ToString() != "")
                {
                    tmpDate = order[3].ToString();
                }
                //if orders exist 
                if (order.Count > 7)
                {
                    //look for "x" cell, if exist make new order for user on that row
                    for (int i = 8; i < order.Count; i++)
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

        public void PlaceOrders(List<Order> orders)
        {
            string today = "06-Feb-2018";//DateTime.Today in productio
            string tomorrow = "07-Feb-2018"; // real in production!!!
            var sheet = GetActiveSheet();
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(LipaSpredSheet, sheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            ValueRange sheetData = request.Execute();

            ValueRange orderMark = new ValueRange();
            IList<object> mark =new  List<object>() {"x"};
            orderMark.Values = new List<IList<object>>() { mark};

            //find index where today menu starts and ends
            int dateStartIndex = 0;
            int dateEndIndex = 0;
            for (int i = 0; i < sheetData.Values[4].Count; i++)
            {
                if (sheetData.Values[3][i].ToString() == today)
                {
                    dateStartIndex = i;
                }
                if (sheetData.Values[3][i].ToString() == tomorrow)
                {
                    dateEndIndex = i - 1;
                    break;
                }
            }

            foreach (var order in orders)
            {
                //find index of row for customer
                int nameIndex = 0;
                string customerAliase = order.Customer.Aliases.First(a => a.Restaurant.Id == restaurant.Id).Alias;
                for (int i = 9; i < sheetData.Values.Count; i++)
                {
                    string tmp = sheetData.Values[i][1].ToString();
                    if (tmp == customerAliase)
                    {
                        nameIndex = i;
                        break;
                    }
                }
                //write 'x' on all orderd foods from order
                foreach (var food in order.Meal.Foods)
                {
                    //find index of food orderd, translate to A notation and write
                    for (int i = dateStartIndex; i < dateEndIndex; i++)
                    {
                        if(sheetData.Values[2][i].ToString() == food.Name)
                        {
                            var cell = GetExcelColumnName(i +1) + (nameIndex +1);//add +1, Google sheets index are 1 based, ValuRange are 0 based

                            var updateRnage = sheet + "!" + cell;



                            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = GoogleSS.Spreadsheets.Values.Update(orderMark, LipaSpredSheet,updateRnage);
                            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

                            updateRequest.Execute();
                        }
                    }
                }



            }

        }

        /// <summary>
        /// Calculates Excel column in alphabet
        /// </summary>
        /// <param name="columnNumber">Int number of column</param>
        /// <returns>Alphabet string</returns>
        private string GetExcelColumnName(int columnNumber)
        {
            if (columnNumber < 0)
            {
                throw new ArgumentException();
            }

            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}
