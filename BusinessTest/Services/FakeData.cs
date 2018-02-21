using System;
using System.Collections.Generic;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest
{

    public static class FakeData
    {
        #region Customer data

        public static Customer customer = new Customer
        {
            Id = 1,
            Name = "TestName",
            Balance = 10,
            Location = new Location
            {
                Id = 1,
                Name = "TestLocationName",
                Address = "TestAdress"
            }
        };

        public static List<Customer> cutumerList = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "TestName1",
                Balance = 10,
                Location = new Location
                {
                    Id = 1,
                    Name = "TestLocationName",
                    Address = "TestAdress"
                }
            },
            new Customer
            {
                Id = 2,
                Name = "TestName2",
                Balance = 10,
                Location = new Location
                {
                    Id = 1,
                    Name = "TestLocationName",
                    Address = "TestAdress"
                }
            },
            new Customer
            {
                Id = 3,
                Name = "TestName3",
                Balance = 10,
                Location = new Location
                {
                    Id = 1,
                    Name = "TestLocationName",
                    Address = "TestAdress"
                }
            }
        };

        #endregion

        #region Food data

        public static Food food = new Food {
            Id = 1,
            Name = "TestFood",
            Price = 1,
            Type = FoodType.MAIN_COURSE,
            Restaurant = new Restaurant
            {
                Id = 1,
                Name = "TestRestaurant",
                DailyMenu = new List<Food>()
            }
        };

        public static List<Food> foodList = new List<Food>
        {
            new Food
            {
                Id = 1,
                Name = "TestFood1",
                Price = 1,
                Type = FoodType.MAIN_COURSE,
                Restaurant = new Restaurant
                {
                    Id = 1,
                    Name = "TestRestaurant1",
                    DailyMenu = new List<Food>()
                }
            },
            new Food
            {
                Id = 2,
                Name = "TestFood2",
                Price = 1,
                Type = FoodType.MAIN_COURSE,
                Restaurant = new Restaurant
                {
                    Id = 1,
                    Name = "TestRestaurant1",
                    DailyMenu = new List<Food>()
                }
            },
            new Food
            {
                Id = 3,
                Name = "TestFood3",
                Price = 1,
                Type = FoodType.SIDE_DISH,
                Restaurant = new Restaurant
                {
                    Id = 1,
                    Name = "TestRestaurant1",
                    DailyMenu = new List<Food>()
                }
            },
            new Food
            {
                Id = 4,
                Name = "TestFood4",
                Price = 1,
                Type = FoodType.MAIN_COURSE,
                Restaurant = new Restaurant
                {
                    Id = 2,
                    Name = "TestRestaurant2",
                    DailyMenu = new List<Food>()
                }
            }
        };

        #endregion

        #region Restaurant data

        #endregion

        #region Order data

        #endregion

        #region Location data

        public static Location location = new Location {
            Id = 1,
            Name = "TestLocation",
            Address = "TestAdress"
        };

        public static List<Location> locationList = new List<Location> {
            new Location
            {
                Id = 1,
                Name = "TestLocation1",
                Address = "TestAdress1"
            },
            new Location
            {
                Id = 2,
                Name = "TestLocation2",
                Address = "TestAdress2"
            },
            new Location
            {
                Id = 3,
                Name = "TestLocation3",
                Address = "TestAdress3"
            }
        };

        #endregion

        #region Menu data

        #endregion
    }
}
