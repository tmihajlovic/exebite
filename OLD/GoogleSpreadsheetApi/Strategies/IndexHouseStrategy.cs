using System;
using System.Collections.Generic;
using Exebite.Model;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;

namespace Exebite.GoogleSpreadsheetApi.Strategies
{
    public class IndexHouseStrategy : BaseRestaurantStrategy, IRestaurantStrategy
    {
        #region Fields

        private string sheetId;

        private string chickenNuggetsName = "chicken nuggets";
        private List<string> chickenNuggetsException = new List<string>
        {
            "mala porcija",
            "velika porcija"
        };

        private string sheetNameOrdering = "Narucivanje";
        private string sheetNameFood = "Opisi jela i cene";
        
        private string[][] mappedOrdersSheet;
        private string[][] mappedFoodSheet;

        private Restaurant restaurant;
        #endregion

        #region Spreadsheet Indexes

        private static readonly int DayRowIndex = 1;
        private static readonly int DayColumnIndex = 1;
        
        private static readonly int DayColumnOffset = 11;

        private static readonly int CustomerRowIndex = 5;
        private static readonly int CustomerColumnIndex = 0;

        private static readonly int FoodRowIndex = 2;
        private static readonly int FoodTypeColumnIndex = 0;
        private static readonly int FoodNameColumnIndex = 1;
        private static readonly int FoodPriceColumnIndex =2;
        private static readonly string CondimentsFlag = "DODACI:";
        #endregion

        public IndexHouseStrategy(IGoogleSheetServiceFactory GoogleSSFactory, IGoogleSpreadsheetIdFactory GoogleSSIdFactory)
            :base(GoogleSSFactory)
        {
            sheetId = GoogleSSIdFactory.GetIndexHouse();
            mappedOrdersSheet = MapFields(sheetId, sheetNameOrdering);
            mappedFoodSheet = MapFields(sheetId, sheetNameFood);
            restaurant = new Restaurant()
            {
                Name = "Index House"
            };
        }

        public List<Food> GetDailyMenu()
        {
            var foodList = new List<Food>();

            var typeOfFood = FoodType.MAIN_COURSE;
            for (int i = FoodRowIndex; i < mappedFoodSheet.Length; i++)
            {
                var name = mappedFoodSheet[i][FoodNameColumnIndex];
                var price = mappedFoodSheet[i][FoodPriceColumnIndex];

                var foodType = mappedFoodSheet[i][FoodTypeColumnIndex];
                if (!string.IsNullOrWhiteSpace(foodType) && foodType.Contains(CondimentsFlag))
                    typeOfFood = FoodType.CONDIMENTS;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (chickenNuggetsException.Any(x => name.Equals(x)))
                    {
                        name = $"{chickenNuggetsName} {chickenNuggetsException.FirstOrDefault(x => name.Equals(x)).ToString()}";
                    }

                    var food = new Food
                    {
                        Name = name,
                        Price = Convert.ToDecimal(price),
                        Type = typeOfFood
                    };

                    foodList.Add(food);
                }
            }

            return foodList;
        }

        public List<Order> GetHistoricalData()
        {
            List<Order> orders = new List<Order>();

            List<Food> foods = GetDailyMenu();

            // Create customer objects
            string [] customerNames = mappedOrdersSheet.ToList().Select(x => x[0]).ToArray();
            List<Customer> customers = customerNames.Select(x => new Customer { Name = x }).ToList();
            
            // Go through all cells in row for dates
            for (int i = 1; i < mappedOrdersSheet[1].Length; i++)
            {
                // Extract data only for 
                if (!string.IsNullOrWhiteSpace(mappedOrdersSheet[1][i]))
                {
                    // Parse string to data
                    var removedDayName = mappedOrdersSheet[1][i].Split()[0];
                    DateTime date = ParseDate(removedDayName);

                    // and extract data
                    string[][] dateValues;

                    // after 11.10.2017 there is one more column
                    // and extract data
                    if (date < new DateTime(2017,11,10))
                    {
                        dateValues = GetDateData(mappedOrdersSheet, i, DayColumnOffset);
                    }
                    else
                    {
                        dateValues = GetDateData(mappedOrdersSheet, i, DayColumnOffset + 1);
                    }
                    
                    // Go through all customers
                    for (int j = CustomerRowIndex; j < dateValues.Length; j++)
                    {
                        // Extract note
                        string note = dateValues[j].Last();

                        // Go through row and extract foods
                        var orderedFoods = new List<Food>();
                        for (int foodIndex = 0; foodIndex < dateValues[j].Length - 1; foodIndex++)
                        {
                            var foodName = dateValues[j][foodIndex];

                            if (!string.IsNullOrWhiteSpace(foodName))
                            {
                                // Split condiments if any
                                var splited = foodName.Split(',');
                                
                                foreach (var split in splited)
                                {
                                    // Extract food
                                    var food = foods.FirstOrDefault(f => split.Contains(f.Name) || f.Name.Contains(split));
                                    if (food != null)
                                    {
                                        // Add to ordered foods balsamiko
                                        food.Restaurant = restaurant;
                                        orderedFoods.Add(food);
                                    }
                                }
                            }
                            
                        }
                        
                        // Check if any
                        if(orderedFoods?.Count > 0)
                        {
                            // Create meal and order
                            var meal = new Meal()
                            { 
                                Foods = orderedFoods
                            };

                            meal.Price = meal.Foods.Sum(f => f.Price);

                            var order = new Order();
                            order.Date = date;
                            order.Meal = meal;
                            order.Customer = customers[j];
                            order.Note = note;

                            orders.Add(order);
                        }

                    }
                    
                }
            }

            return orders;
        }



        public void PlaceOrders(List<Order> order)
        {
            throw new NotImplementedException();
        }
    }
}
