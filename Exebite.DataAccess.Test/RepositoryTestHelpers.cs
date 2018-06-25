#pragma warning disable SA1124 // Do not use regions
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.Common;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Exebite.DomainModel;
using Microsoft.Extensions.Logging;
using Moq;

namespace Exebite.DataAccess.Test
{
    internal static class RepositoryTestHelpers
    {
        private static readonly IMapper _mapper;
        private static readonly IGetDateTime _dateTime;

        static RepositoryTestHelpers()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            Mapper.Initialize(cfg => cfg.AddProfile<DataAccessMappingProfile>());

            _dateTime = new GetDateTimeStub();
            _mapper = Mapper.Instance;
        }

        #region Customer
        internal static CustomerRepository FillCustomerDataForTesting(string name, IEnumerable<CustomerEntity> customers)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return new CustomerRepository(factory, _mapper, new Mock<ILogger<CustomerRepository>>().Object);
        }

        internal static IEnumerable<CustomerEntity> CreateCustomerEntities(int numberOfCustomers)
        {
            return Enumerable.Range(1, numberOfCustomers)
                .Select(x => new CustomerEntity()
                {
                    Id = x,
                    Balance = x,
                    AppUserId = (1000 + x).ToString(),
                    LocationId = x,
                    Name = $"Name {x}"
                });
        }

        internal static IEnumerable<Customer> CreateCustomers(int startId, int numberOfCustomers)
        {
            return Enumerable.Range(startId, numberOfCustomers)
                            .Select(x => new Customer()
                            {
                                Id = x,
                                Balance = x,
                                AppUserId = (1000 + x).ToString(),
                                LocationId = x,
                                Name = $"Name {x}"
                            });
        }

        #endregion

        #region Location
        internal static LocationRepository LocationDataForTesing(string name, int numberOfLocations)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, numberOfLocations).Select(x => new LocationEntity()
                {
                    Id = x,
                    Address = $"Address {x}",
                    Name = $"Name {x}"
                });

                context.Locations.AddRange(locations);
                context.SaveChanges();
            }

            return new LocationRepository(factory, _mapper, new Mock<ILogger<LocationRepository>>().Object);
        }

        internal static LocationRepository CreateOnlyLocationRepositoryInstanceNoData(string name)
        {
            return new LocationRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<LocationRepository>>().Object);
        }

        #endregion Location

        #region CustomerAlias
        internal static CustomerAliasRepository CustomerAliasesDataForTesing(string name, int numberOfCustomerAliases)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                var customerAlias = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new CustomerAliasesEntities
                {
                    Id = x,
                    Alias = $"Alias {x}",
                    CustomerId = x,
                    RestaurantId = x
                });
                context.CustomerAliases.AddRange(customerAlias);

                var restaurant = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurants.AddRange(restaurant);

                var customers = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new CustomerEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return new CustomerAliasRepository(factory, _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }

        internal static CustomerAliasRepository CreateOnlyCustomerAliasRepositoryInstanceNoData(string name)
        {
            return new CustomerAliasRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }
        #endregion

        #region Food
        internal static FoodRepository CreateOnlyFoodRepositoryInstanceNoData(string name)
        {
            return new FoodRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<FoodRepository>>().Object);
        }

        internal static FoodRepository FoodDataForTesting(string name, int numberOfFoods)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                context.Restaurants.Add(new RestaurantEntity()
                {
                    Id = 1,
                    Name = "Test restaurant"
                });

                context.DailyMenues.Add(new DailyMenuEntity()
                {
                    Id = 1,
                    RestaurantId = 1
                });

                var foods = Enumerable.Range(1, numberOfFoods).Select(x => new FoodEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    Description = $"Description {x}",
                    Price = x,
                    Type = FoodType.MAIN_COURSE,
                    RestaurantId = 1
                });

                context.Foods.AddRange(foods);
                context.SaveChanges();
            }

            return new FoodRepository(factory, _mapper, new Mock<ILogger<FoodRepository>>().Object);
        }
        #endregion Food

        #region Restaurant
        internal static RestaurantRepository RestaurantDataForTesting(string name, int count)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count).Select(x => new RestaurantEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    DailyMenu = new DailyMenuEntity()
                    {
                        Id = x
                    }
                });

                context.Restaurants.AddRange(locations);
                context.SaveChanges();
            }

            return new RestaurantRepository(factory, _mapper, new Mock<ILogger<RestaurantRepository>>().Object);
        }

        internal static CustomerAliasRepository CreateOnlyRestaurantRepositoryInstanceNoData(string name)
        {
            return new CustomerAliasRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }

        #endregion

        #region DailyMenu
        internal static DailyMenuRepository DailyMenuDataForTesing(Guid name, int numberOfDailyMenus)
        {
            var factory = new InMemoryDBFactory(name.ToString());

            using (var context = factory.Create())
            {
                var dailyMenus = Enumerable.Range(1, numberOfDailyMenus).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenues.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, numberOfDailyMenus).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurants.AddRange(restaurant);

                var food = Enumerable.Range(1, numberOfDailyMenus).Select(x => new FoodEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Price = x,
                    Description = $"Description {x}"
                });
                context.Foods.AddRange(food);
                context.SaveChanges();
            }

            return new DailyMenuRepository(factory, _mapper, new Mock<ILogger<DailyMenuRepository>>().Object);
        }

        internal static DailyMenuRepository CreateOnlyDailyMenuRepositoryInstanceNoData(Guid name)
        {
            return new DailyMenuRepository(new InMemoryDBFactory(name.ToString()), _mapper, new Mock<ILogger<DailyMenuRepository>>().Object);
        }
        #endregion DailyMenu

        #region Meal
        internal static MealRepository MealDataForTesing(string name, int numberOfMeals)
        {
            var factory = new InMemoryDBFactory(name);

            using (var context = factory.Create())
            {
                context.Restaurants.Add(new RestaurantEntity()
                {
                    Id = 1,
                    Name = "Test restaurant"
                });

                var foods = Enumerable.Range(1, numberOfMeals).Select(x => new FoodEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    Description = $"Description {x}",
                    Price = x,
                    Type = FoodType.MAIN_COURSE,
                    RestaurantId = 1
                });

                context.Foods.AddRange(foods);

                var meals = Enumerable.Range(1, numberOfMeals).Select(x => new MealEntity
                {
                    Id = x,
                    Price = x,
                    FoodEntityMealEntities = new List<FoodEntityMealEntities>
                    {
                        new FoodEntityMealEntities { FoodEntityId = x }
                    }
                });

                context.Meals.AddRange(meals);
                context.SaveChanges();
            }

            return new MealRepository(factory, _mapper, new Mock<ILogger<MealRepository>>().Object);
        }

        internal static MealRepository CreateOnlyMealRepositoryInstanceNoData(string name)
        {
            return new MealRepository(new InMemoryDBFactory(name), _mapper, new Mock<ILogger<MealRepository>>().Object);
        }
        #endregion

        #region Recipe

        internal static RecipeRepository RecipeDataForTesting(Guid name, int numberOfDailyRecipes)
        {
            var factory = new InMemoryDBFactory(name.ToString());

            using (var context = factory.Create())
            {
                var dailyMenus = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new RecipeEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.Recipes.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = "Test restaurant " + x,
                    DailyMenuId = x,
                });
                context.Restaurants.AddRange(restaurant);

                var food = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new FoodEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Price = x,
                    Description = $"Description {x}"
                });
                context.Foods.AddRange(food);
                context.SaveChanges();
            }

            return new RecipeRepository(factory, _mapper, new Mock<ILogger<RecipeRepository>>().Object);
        }

        internal static RecipeRepository CreateOnlyRecipeRepositoryInstanceNoData(Guid name)
        {
            return new RecipeRepository(new InMemoryDBFactory(name.ToString()), _mapper, new Mock<ILogger<RecipeRepository>>().Object);
        }
        #endregion Recipe

        #region Order
        internal static OrderRepository OrderDataForTesting(Guid name, int numberOfOrders)
        {
            var factory = new InMemoryDBFactory(name.ToString());

            using (var context = factory.Create())
            {
                var location = new LocationEntity
                {
                    Id = 1,
                    Name = "location name ",
                    Address = "Address"
                };

                context.Locations.Add(location);

                var customer = new CustomerEntity
                {
                    Id = 1,
                    Name = "Customer name ",
                    AppUserId = "AppUserId",
                    Balance = 99.99m,
                    LocationId = 1,
                };
                context.Customers.Add(customer);

                var orders = Enumerable.Range(1, numberOfOrders).Select(x => new OrderEntity
                {
                    Id = x,
                    CustomerId = x,
                    Date = _dateTime.Now().AddHours(x),
                    MealId = x,
                    Note = "note ",
                    Price = 10.5m * x
                });

                context.Orders.AddRange(orders);

                context.SaveChanges();
            }

            return new OrderRepository(factory, _mapper, new Mock<ILogger<OrderRepository>>().Object);
        }

        internal static OrderRepository CreateOnlyOrderRepositoryInstanceNoData(Guid name)
        {
            return new OrderRepository(new InMemoryDBFactory(name.ToString()), _mapper, new Mock<ILogger<OrderRepository>>().Object);
        }
        #endregion Recipe
    }
}
#pragma warning restore SA1124 // Do not use regions