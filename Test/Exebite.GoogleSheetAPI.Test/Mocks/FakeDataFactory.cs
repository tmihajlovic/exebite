using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Test.Mocks
{
    public class FakeDataFactory
    {
        private readonly Restaurant _restaurant;

        private readonly List<Location> locations = new List<Location>()
        {
            new Location
            {
                Id = 1,
                Name = "Bulevar",
                Address = "Bulevar Vojvode Stjepe"
            },
            new Location
            {
                Id = 2,
                Name = "JS",
                Address = "Jovana Ducica"
            }
        };

        private readonly List<Restaurant> restaurants = new List<Restaurant>()
        {
            new Restaurant
            {
                Id = 1,
                Name = "Restoran pod Lipom"
            },
            new Restaurant
            {
                Id = 2,
                Name = "Teglas"
            },
            new Restaurant
            {
                Id = 1,
                Name = "Hedone"
            }
        };

        private readonly List<Customer> customers = new List<Customer>();

        private readonly List<Food> foods = new List<Food>();

        private readonly List<Order> orders = new List<Order>();

        public FakeDataFactory(string restaurantName)
        {
            _restaurant = restaurants.First(r => r.Name == restaurantName);
            InitFood();
            InitCustomers();
            IniOrders();
        }

        public List<Food> GetFoods()
        {
            return foods;
        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }

        private void InitFood()
        {
            foods.Add(new Food
            {
                Id = 1,
                Name = "Food 1",
                Description = "Description 1",
                Restaurant = _restaurant,
                Price = 100,
                IsInactive = false,
                Type = FoodType.MAIN_COURSE
            });
            foods.Add(new Food
            {
                Id = 2,
                Name = "Food 2",
                Description = "Description 2",
                Restaurant = _restaurant,
                Price = 200,
                IsInactive = false,
                Type = FoodType.MAIN_COURSE
            });
            foods.Add(new Food
            {
                Id = 3,
                Name = "Food 3",
                Description = "Description 3",
                Restaurant = _restaurant,
                Price = 300,
                IsInactive = false,
                Type = FoodType.DESERT
            });
        }

        private void InitCustomers()
        {
            customers.Add(new Customer
            {
                Id = 1,
                Balance = 1000,
                Location = locations.First(),
                Name = "Test Customer 1",
                Orders = new List<Order>()
            });
            customers.Add(new Customer
            {
                Id = 2,
                Balance = 1000,
                Location = locations.First(),
                Name = "Test Customer 2",
                Orders = new List<Order>()
            });
            customers.Add(new Customer
            {
                Id = 3,
                Balance = 1000,
                Location = locations.First(),
                Name = "Test Customer 3",
                Orders = new List<Order>()
            });
        }

        private void IniOrders()
        {
            orders.Add(new Order
            {
                Id = 1,
                Date = DateTime.Today.Date,
                Note = "Test note 1",
                Price = 100,
                Customer = customers.First(),
                Meal = new Meal
                {
                    Id = 1,
                    Price = 100,
                    Foods = new List<Food>
                        {
                            foods[0]
                        }
                 }
            });
            orders.Add(new Order
            {
                Id = 3,
                Date = DateTime.Today.Date,
                Note = "Test note 2",
                Price = 100,
                Customer = customers.First(),
                Meal = new Meal
                {
                    Id = 1,
                    Price = 300,
                    Foods = new List<Food>
                        {
                            foods[2], foods[1]
                        }
                }
            });
            orders.Add(new Order
            {
                Id = 2,
                Date = DateTime.Today.Date,
                Note = "Test note 3",
                Price = 100,
                Customer = customers.First(),
                Meal = new Meal
                {
                    Id = 1,
                    Price = 100,
                    Foods = new List<Food>
                        {
                            foods[2]
                        }
                }
            });

            customers.First().Orders = orders;
        }
    }
}
