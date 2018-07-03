#pragma warning disable SA1124 // Do not use regions
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Exebite.DomainModel;
using Microsoft.Data.Sqlite;
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
        internal static CustomerQueryRepository CreateOnlyCustomerQueryRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new CustomerQueryRepository(factory, _mapper);
        }

        internal static CustomerCommandRepository CreateOnlyCustomerCommandRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new CustomerCommandRepository(factory, _mapper);
        }
        #endregion

        #region Location
        internal static LocationQueryRepository CreateOnlyLocationQueryRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new LocationQueryRepository(factory, _mapper);
        }

        internal static LocationCommandRepository CreateOnlyLocationCommandRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new LocationCommandRepository(factory, _mapper);
        }

        #endregion Location

        #region CustomerAlias
        internal static CustomerAliasRepository CustomerAliasesDataForTesing(SqliteConnection connection, int numberOfCustomerAliases)
        {
            var factory = new InMemoryDBFactory(connection);

            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new LocationEntity()
                {
                    Id = x,
                    Address = $"Address {x}",
                    Name = $"Name {x}"
                });

                context.Locations.AddRange(locations);
                var customerAlias = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new CustomerAliasesEntities
                {
                    Id = x,
                    Alias = $"Alias {x}",
                    CustomerId = x,
                    RestaurantId = x
                });
                context.CustomerAliases.AddRange(customerAlias);

                var dailyMenus = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenues.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurants.AddRange(restaurant);

                var customers = Enumerable.Range(1, numberOfCustomerAliases).Select(x => new CustomerEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    LocationId = x
                });
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return new CustomerAliasRepository(factory, _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }

        internal static CustomerAliasRepository CreateOnlyCustomerAliasRepositoryInstanceNoData(SqliteConnection connection)
        {
            return new CustomerAliasRepository(new InMemoryDBFactory(connection), _mapper, new Mock<ILogger<CustomerAliasRepository>>().Object);
        }
        #endregion

        #region Food
        internal static FoodRepository CreateOnlyFoodRepositoryInstanceNoData(SqliteConnection connection)
        {
            return new FoodRepository(new InMemoryDBFactory(connection), _mapper, new Mock<ILogger<FoodRepository>>().Object);
        }

        internal static FoodRepository FoodDataForTesting(SqliteConnection connection, int numberOfFoods)
        {
            var factory = new InMemoryDBFactory(connection);

            using (var context = factory.Create())
            {
                var restaurants = Enumerable.Range(1, numberOfFoods).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Test restaurant {x}"
                });
                context.Restaurants.AddRange(restaurants);

                var dailyMenues = Enumerable.Range(1, numberOfFoods).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenues.AddRange(dailyMenues);

                var foods = Enumerable.Range(1, numberOfFoods).Select(x => new FoodEntity
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
        internal static RestaurantQueryRepository CreateOnlyRestaurantQueryRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new RestaurantQueryRepository(factory, _mapper);
        }

        internal static RestaurantCommandRepository CreateOnlyRestaurantCommandRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new RestaurantCommandRepository(factory, _mapper);
        }

        #endregion

        #region DailyMenu
        internal static DailyMenuRepository DailyMenuDataForTesing(SqliteConnection connection, int numberOfDailyMenus)
        {
            var factory = new InMemoryDBFactory(connection);

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
                    Description = $"Description {x}",
                    RestaurantId = x
                });
                context.Foods.AddRange(food);
                context.SaveChanges();
            }

            return new DailyMenuRepository(factory, _mapper, new Mock<ILogger<DailyMenuRepository>>().Object);
        }

        internal static DailyMenuRepository CreateOnlyDailyMenuRepositoryInstanceNoData(SqliteConnection connection)
        {
            return new DailyMenuRepository(new InMemoryDBFactory(connection), _mapper, new Mock<ILogger<DailyMenuRepository>>().Object);
        }
        #endregion DailyMenu

        #region Meal
        internal static MealRepository MealDataForTesing(SqliteConnection connection, int numberOfMeals)
        {
            var factory = new InMemoryDBFactory(connection);

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

        internal static MealRepository CreateOnlyMealRepositoryInstanceNoData(SqliteConnection connection)
        {
            return new MealRepository(new InMemoryDBFactory(connection), _mapper, new Mock<ILogger<MealRepository>>().Object);
        }
        #endregion

        #region Recipe

        internal static RecipeRepository RecipeDataForTesting(SqliteConnection connection, int numberOfDailyRecipes)
        {
            var factory = new InMemoryDBFactory(connection);

            using (var context = factory.Create())
            {
                var recipes = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new RecipeEntity
                {
                    Id = x,
                    RestaurantId = x,
                    MainCourseId = x
                });
                context.Recipes.AddRange(recipes);

                var dailyMenus = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenues.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = "Test restaurant " + x
                });
                context.Restaurants.AddRange(restaurant);

                var food = Enumerable.Range(1, numberOfDailyRecipes).Select(x => new FoodEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Price = x,
                    Description = $"Description {x}",
                    RestaurantId = x
                });
                context.Foods.AddRange(food);
                context.SaveChanges();
            }

            return new RecipeRepository(factory, _mapper, new Mock<ILogger<RecipeRepository>>().Object);
        }

        internal static RecipeRepository CreateOnlyRecipeRepositoryInstanceNoData(SqliteConnection connection)
        {
            return new RecipeRepository(new InMemoryDBFactory(connection), _mapper, new Mock<ILogger<RecipeRepository>>().Object);
        }
        #endregion Recipe

        #region Order
        internal static OrderRepository OrderDataForTesting(SqliteConnection connection, int numberOfOrders)
        {
            var factory = new InMemoryDBFactory(connection);

            using (var context = factory.Create())
            {
                var location = new LocationEntity
                {
                    Id = 1,
                    Name = "location name ",
                    Address = "Address"
                };

                context.Locations.Add(location);

                var customers = Enumerable.Range(1, numberOfOrders).Select(x => new CustomerEntity
                {
                    Id = x,
                    Name = "Customer name ",
                    AppUserId = "AppUserId",
                    Balance = 99.99m,
                    LocationId = 1,
                });
                context.Customers.AddRange(customers);

                var meals = Enumerable.Range(1, numberOfOrders).Select(x => new MealEntity
                {
                    Id = x,
                    Price = 3.2m * x
                });
                context.Meals.AddRange(meals);

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

        internal static OrderRepository CreateOnlyOrderRepositoryInstanceNoData(SqliteConnection connection)
        {
            return new OrderRepository(new InMemoryDBFactory(connection), _mapper, new Mock<ILogger<OrderRepository>>().Object);
        }
        #endregion Recipe
    }
}
#pragma warning restore SA1124 // Do not use regions