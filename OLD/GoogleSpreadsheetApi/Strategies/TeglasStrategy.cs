using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.Model;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSpreadsheetApi.Strategies
{
    public class TeglasStrategy : IRestaurantStrategy
    {
        private string TeglasSpredSheet;
        private Restaurant restaurant;
        SheetsService GoogleSS;
        
        public TeglasStrategy(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
        {
            restaurant = new Restaurant { Name = "Teglas" };
            GoogleSS = GoogleSSFactory.GetService();
            TeglasSpredSheet = GoogleSSIdFactory.GetTeglas();
        }


        /// <summary>
        /// Get menu for today
        /// </summary>
        /// <returns>List of Food available</returns>
        public List<Food> GetDailyMenu()
        {
            List<Food> foodList = new List<Food>();
            var sheet = "Opisi salata i cene";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(TeglasSpredSheet, sheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            ValueRange sheetData = request.Execute();

            var foodData = sheetData.Values.Skip(1).Where(i => i.Count == 4);
            foreach(var item in foodData)
            {
                foodList.Add(new Food {
                    Name = item[0].ToString(),
                    Type = FoodType.MAIN_COURSE,
                    Price = decimal.Parse(item[2].ToString()),
                    Description = item[1].ToString(),
                    IsInactive = false
                    
                });
            }

            return foodList;
        }

        public List<Order> GetHistoricalData()
        {
            List<Order> orderList = new List<Order>();
            var sheet = "Narucivanje";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        GoogleSS.Spreadsheets.Values.Get(TeglasSpredSheet, sheet);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange sheetData = request.Execute();

            List<Food> foodList = this.GetDailyMenu();
            var foodData = sheetData.Values;
            var currentYear = 2017;
            var currentMonth = 1;
            
            for(int i = 1; i< foodData.Count(); i+=4)//4 column loop
            {
                if(foodData[i].Count > 4)//oreder exist on this column
                {
                    for (int k = 4; k< foodData[i].Count(); k++)//row loop
                    {
                        if (foodData[i][k].ToString() != "" )//find oreder
                        {
                            var newOrder = new Order
                            {
                                Meal = new Meal
                                {
                                    Foods = new List<Food>()
                                }
                            };
                            //generate date
                            var dateString = foodData[i][2].ToString();
                            dateString = new String(dateString.TakeWhile(c => !char.IsLetter(c)).ToArray()) + currentYear;
                            DateTime date = DateTime.Parse(dateString);
                            if(date.Month < currentMonth) //year advance
                            {
                                currentMonth = date.Month;
                                currentYear++;
                                newOrder.Date = new DateTime(currentYear, date.Month, date.Day);
                            }
                            else
                            {
                                currentMonth = date.Month;
                                newOrder.Date = new DateTime(currentYear, date.Month, date.Day);
                            }
                            //shet typo sometimes there is '(' before teglas salad
                            if (foodData[i][k].ToString() == "(Teglas Salad (320g)")
                            {
                                foodData[i][k] = "Teglas Salad (320g)";
                            }
                            //add food to meal
                            var tmpFood = foodList.FirstOrDefault(f => f.Name == foodData[i][k].ToString());
                            if (tmpFood != null)
                            {
                                tmpFood.Restaurant = restaurant;
                                newOrder.Meal.Foods.Add(tmpFood);
                            }
                            //check 2nd column of a order
                            if (foodData[i+1].Count > k && foodData[i+ 1][k].ToString() != "")
                            {
                                tmpFood = foodList.FirstOrDefault(f => f.Name == foodData[i + 1][k].ToString());
                                if (tmpFood != null)
                                {
                                    tmpFood.Restaurant = restaurant;
                                    newOrder.Meal.Foods.Add(tmpFood);
                                }
                            }
                            // check 3th  column of a order
                            if (foodData[i + 2].Count > k && foodData[i + 2][k].ToString() != "")
                            {
                                tmpFood = foodList.FirstOrDefault(f => f.Name == foodData[i + 2][k].ToString());
                                if (tmpFood != null)
                                {
                                    tmpFood.Restaurant = restaurant;
                                    newOrder.Meal.Foods.Add(tmpFood);
                                }
                            }
                            //check for note
                            if (foodData[i + 3].Count > k && foodData[i + 3][k].ToString() != "")
                            {
                                newOrder.Note = foodData[i + 3][k].ToString();
                            }
                            //calculate price
                            newOrder.Meal.Price = newOrder.Meal.Foods.Sum(f => f.Price);
                            newOrder.Price = newOrder.Meal.Price;
                            //get customer name
                            newOrder.Customer = new Customer { Name = foodData[0][k].ToString() };
                            //add oreder to list
                            if(newOrder.Meal.Foods.Count != 0)
                            orderList.Add(newOrder);
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
