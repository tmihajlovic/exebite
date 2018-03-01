using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.Model;

namespace Exebite.GoogleSpreadsheetApi.Strategies
{
    public class ExtraFoodStrategy : BaseRestaurantStrategy, IRestaurantStrategy
    {
        private string sheetId;
        private string sheetFoodListName = "Jela";
        private string sheetSideDishListName = "Prilozi, supe, salate i deserti";
        private string sheetPrices = "Cene";
        private string sheetFoodByOrder = "Jela po porudzbini";
        private string sheetDailyOrders = "Dnevna ponuda ";

        private string[][] mappedFoods;
        private string[][] mappedSidedishes;
        private string[][] mappedPrices;
        private string[][] mappedFoodByOrder;
        private string[][] mappedDailyOrders;

        private Restaurant restaurant;
        private static readonly int FoodByOrderRow = 30;
        private static readonly string DailySideDishColumn = "J";
        private static readonly string AlwaysAvailableSideDishColumn = "D";
        private static readonly int SideDishAndDesertRowStart = 3;

        private static readonly int PricesRowStart = 6;
        private static readonly int PricesMaxRow = 14;
        private static readonly string PricesColumnStart = "F";

        private static readonly int DailyMenuMaximumNumberRow = 27;

        private static readonly int DayColumnOffset = 6;

        private static readonly int CustomerRowIndex = 2;

        public ExtraFoodStrategy(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
            :base(GoogleSSFactory)
        {
            sheetId = GoogleSSIdFactory.GetExtraFood();
            mappedFoods = MapFields(sheetId, sheetFoodListName);
            mappedSidedishes = MapFields(sheetId, sheetSideDishListName);
            mappedPrices = MapFields(sheetId, sheetPrices);

            mappedDailyOrders = MapFields(sheetId, sheetDailyOrders);
            mappedFoodByOrder = MapFields(sheetId, sheetFoodByOrder);

            restaurant = new Restaurant
            {
                Name = "Extra Food"
            };

        }

        /// <summary>
        /// Get daily menu
        /// </summary>
        /// <returns>Returns daily menu</returns>
        public List<Food> GetDailyMenu()
        {
            Dictionary<string,decimal> namePriceKeyValue = GetSideDishesNameAndPrices();

            var foods = new List<Food>();

            int dayOfWeek = (int)DateTime.Now.Date.DayOfWeek;
            foods.AddRange(GetDailyFoods(dayOfWeek));
            foods.AddRange(GetDailySideDishes(dayOfWeek));
            foods.AddRange(GetAlwaysAvailableMenu());

            var foodWithoutPrices = foods.Where(f => f.Price == 0).ToList();

            foreach (var item in foodWithoutPrices)
            {
                if (namePriceKeyValue.ContainsKey(item.Name))
                {
                    item.Price = namePriceKeyValue[item.Name];
                }
            }

            return foods;
        }

        /// <summary>
        /// Get historical data
        /// </summary>
        /// <returns>List of orders</returns>
        public List<Order> GetHistoricalData()
        {
            var orders = new List<Order>();

            orders.AddRange(GetHistoricalDataDailyMenu());
            orders.AddRange(GetHistoricalDataFoodByOrder());

            return orders;
        }

        public void PlaceOrders(List<Order> order)
        {
            throw new NotImplementedException();
        }

        private List<Food> GetAlwaysAvailableMenu()
        {
            Dictionary<string, decimal> namePriceKeyValue = GetSideDishesNameAndPrices();

            var foods = new List<Food>();

            foods.AddRange(GetAlwaysAvailableFoods());
            foods.AddRange(GetAlwaysAvailableSideDish());

            return foods;
        }

        private List<Order> GetHistoricalDataFoodByOrder()
        {
            var orders = new List<Order>();

            var alwaysAvailableFood = GetAlwaysAvailableMenu();

            // Create customer objects
            string[] customerNames = mappedFoodByOrder.ToList().Select(x => x[0]).ToArray();
            List<Customer> customers = customerNames.Select(x => new Customer { Name = x }).ToList();

            for (int i = 0; i < mappedFoodByOrder[1].Length; i++)
            {
                // Extract data only for 
                if (!string.IsNullOrWhiteSpace(mappedFoodByOrder[0][i]))
                {
                    var removedDayName = "";
                    if (mappedFoodByOrder[0][i].IndexOf('(') > -1)
                    {
                        // Parse string to data
                        removedDayName = mappedFoodByOrder[0][i].Remove(0, mappedFoodByOrder[0][i].IndexOf('(') + 1);
                        removedDayName = removedDayName.Remove(removedDayName.IndexOf(')'));
                    }
                    else
                    {
                        removedDayName = mappedFoodByOrder[0][i].Split()[1];
                    }

                    DateTime date = ParseDate(removedDayName);

                    // and extract data
                    string[][] dateValues = GetDateData(mappedFoodByOrder, i, 2);
                    

                    for (int  j = CustomerRowIndex; j < dateValues.Length; j++)
                    {
                        if (string.IsNullOrWhiteSpace(dateValues[j][0]))
                        {
                            continue;
                        }
                        
                        var food = alwaysAvailableFood.FirstOrDefault(x => x.Name == dateValues[j][0]);
                        if(food != null)
                        {
                            food.Restaurant = restaurant;
                            var order = new Order()
                            {
                                Customer = customers[j],
                                Date = date
                            };

                            var meal = new Meal
                            {
                                Foods = new List<Food> { food }
                            };

                            order.Meal = meal;

                            orders.Add(order);
                        }
                        
                    }


                }
            }

            return orders;
        }

        private List<Order> GetHistoricalDataDailyMenu()
        {
            var orders = new List<Order>();

            // Create customer objects
            string[] customerNames = mappedDailyOrders.ToList().Select(x => x[0]).ToArray();
            List<Customer> customers = customerNames.Select(x => new Customer { Name = x }).ToList();

            for (int i = 0; i < mappedDailyOrders[1].Length; i++)
            {
                // Extract data only for 
                if (!string.IsNullOrWhiteSpace(mappedDailyOrders[0][i]))
                {
                    // Parse string to data
                    var removedDayName = mappedDailyOrders[0][i].Split(',')[1];
                    DateTime date = ParseDate(removedDayName);

                    List<Food> dateFood = new List<Food>();

                    dateFood.AddRange(GetDailyFoods((int)date.DayOfWeek));
                    dateFood.AddRange(GetDailySideDishes((int)date.DayOfWeek));
                    dateFood.AddRange(GetAlwaysAvailableSideDish());
                    // and extract data
                    string[][] dateValues = GetDateData(mappedDailyOrders, i, DayColumnOffset);

                    for (int j = CustomerRowIndex; j < dateValues.Length; j++)
                    {
                        var orderedFood = new List<Food>();
                        
                        if (dateValues[j].All(x => string.IsNullOrWhiteSpace(x)))
                        {
                            continue;
                        }

                        for (int foodIndex = 0; foodIndex < dateValues[j].Length; foodIndex++)
                        {
                            var foodName = dateValues[j][foodIndex];

                            if (!string.IsNullOrWhiteSpace(foodName))
                            {
                                var foundedFood = dateFood.FirstOrDefault(f => f.Name == foodName);
                                if(foundedFood != null)
                                {
                                    foundedFood.Restaurant = restaurant;
                                    orderedFood.Add(foundedFood);
                                }
                            }
                        }

                        if (orderedFood.Count > 0)
                        {
                            var order = new Order()
                            {
                                Customer = customers[j],
                                Date = date
                            };

                            var meal = new Meal
                            {
                                Foods = orderedFood
                            };

                            order.Meal = meal;

                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }

        private List<Food> GetDailyFoods(int dayOfWeek)
        {
            var todayFoods = new List<Food>();


            for (int i = 1; i < DailyMenuMaximumNumberRow; i++)
            {
                if (!string.IsNullOrWhiteSpace(mappedFoods[i][dayOfWeek - 1]))
                {
                    todayFoods.Add(new Food
                    {
                        Name = mappedFoods[i][dayOfWeek - 1],
                        Type = FoodType.MAIN_COURSE
                    });
                }
            }

            return todayFoods;
        }

        private List<Food> GetDailySideDishes(int dayOfWeek)
        {
            int columnNumber = GetExcelColumnNumber(DailySideDishColumn);

            var dailySideDishes = new List<Food>();

            var dishName = mappedSidedishes[dayOfWeek + SideDishAndDesertRowStart - 1][columnNumber - 1];
           
            var soup = new Food
            {
                Name = dishName,
                Type = FoodType.SOUP
            };

            dailySideDishes.Add(soup);
            

            var desert = new Food
            {
                Name = mappedSidedishes[dayOfWeek + SideDishAndDesertRowStart - 1][columnNumber + 1],
                Type = FoodType.DESERT
            };

            dailySideDishes.Add(desert);

            return dailySideDishes;
        }
        
        private List<Food> GetAlwaysAvailableFoods()
        {
            var orderFoods = new List<Food>();

            for (int i = FoodByOrderRow; i < mappedFoods.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(mappedFoods[i][0]))
                {
                    orderFoods.Add(new Food
                    {
                        Name = mappedFoods[i][0],
                        Type = FoodType.MAIN_COURSE,
                        Price = Convert.ToDecimal(mappedFoods[i][1])
                    });
                }
            }

            return orderFoods;
        }

        private List<Food> GetAlwaysAvailableSideDish()
        {
            int columnNumber = GetExcelColumnNumber(AlwaysAvailableSideDishColumn);

            var alwaysAvailableSideDishes = new List<Food>();

            for (int i = SideDishAndDesertRowStart; i < mappedSidedishes.Length; i++)
            {
                var sideDishName = mappedSidedishes[i][columnNumber - 1];
                if (!string.IsNullOrWhiteSpace(sideDishName))
                {
                    alwaysAvailableSideDishes.Add(new Food
                    {
                        Name = sideDishName,
                        Type = FoodType.SIDE_DISH,
                    });
                }
                
                var saladName = mappedSidedishes[i][columnNumber + 1];
                if (!string.IsNullOrWhiteSpace(saladName))
                {
                    alwaysAvailableSideDishes.Add(new Food
                    {
                        Name = saladName,
                        Type = FoodType.SALAD,
                    });
                }
            }

            return alwaysAvailableSideDishes;
        }

        private Dictionary<string, decimal> GetSideDishesNameAndPrices()
        {
            var namePriceKeyValue = new Dictionary<string, decimal>();
            for (int i = PricesRowStart - 1; i < PricesMaxRow; i++)
            {
                int columnIndex  = GetExcelColumnNumber(PricesColumnStart) - 1;

                for (int j = 0; j < 2; j++)
                {
                    var name = mappedPrices[i][columnIndex + 2*j];
                    var price = mappedPrices[i][columnIndex + 2*j + 1];
                    if (!(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(price)))
                    {
                        namePriceKeyValue.Add(name, Convert.ToDecimal(price));
                    }
                }
            }

            return namePriceKeyValue;
        }
    }
}
