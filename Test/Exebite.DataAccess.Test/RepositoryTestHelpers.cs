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
        internal static CustomerAliasCommandRepository CreateCustomerAliasCommandRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new CustomerAliasCommandRepository(factory, _mapper);
        }

        internal static CustomerAliasQueryRepository CreateCustomerAliasQueryRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new CustomerAliasQueryRepository(factory, _mapper);
        }
        #endregion

        #region Food
        internal static IFoodQueryRepository CreateOnlyFoodRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new FoodQueryRepository(factory, _mapper);
        }

        internal static IFoodQueryRepository FoodDataForTesting(SqliteConnection connection, int numberOfFoods)
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

            return new FoodQueryRepository(factory, _mapper);
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
        internal static DailyMenuQueryRepository CreateDailyMenuQueryRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new DailyMenuQueryRepository(factory, _mapper);
        }

        internal static DailyMenuCommandRepository CreateDailyMenuCommandRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new DailyMenuCommandRepository(factory, _mapper);
        }

        #endregion DailyMenu

        #region Meal
        internal static MealQueryRepository CreateMealQueryRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new MealQueryRepository(factory, _mapper);
        }

        internal static MealCommandRepository CreateMealCommandRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new MealCommandRepository(factory, _mapper);
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
        internal static OrderCommandRepository CreateOrderCommandRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new OrderCommandRepository(factory, _mapper);
        }

        internal static OrderQueryRepository CreateOrderQueryRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new OrderQueryRepository(factory, _mapper);
        }
        #endregion Order
    }
}
#pragma warning restore SA1124 // Do not use regions