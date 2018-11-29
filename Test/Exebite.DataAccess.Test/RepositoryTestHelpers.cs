#pragma warning disable SA1124 // Do not use regions
using AutoMapper;
using AutoMapper.Configuration;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;

namespace Exebite.DataAccess.Test
{
    internal static class RepositoryTestHelpers
    {
        private static readonly IMapper _mapper;
        private static readonly IGetDateTime _dateTime;

        static RepositoryTestHelpers()
        {
            _dateTime = new GetDateTimeStub();

            Mapper.Reset();
            var configExpresion = new MapperConfigurationExpression();
            configExpresion.AddProfile<DataAccessMappingProfile>();
            var config = new MapperConfiguration(configExpresion);
            _mapper = new Mapper(config) as IMapper;
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

        #region Payment

        internal static PaymentQueryRepository CreateOnlyPaymentQueryRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new PaymentQueryRepository(factory, _mapper);
        }

        internal static PaymentCommandRepository CreateOnlyPaymentCommandRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new PaymentCommandRepository(factory, _mapper);
        }
        #endregion Payment

        #region Recipe

        internal static RecipeQueryRepository CreateOnlyRecipeQueryRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new RecipeQueryRepository(factory, _mapper);
        }

        internal static RecipeCommandRepository CreateOnlyRecipeCommandRepositoryInstanceNoData(IFoodOrderingContextFactory factory)
        {
            return new RecipeCommandRepository(factory, _mapper);
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

        #region Role
        internal static RoleQueryRepository CreateRoleQueryRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new RoleQueryRepository(factory, _mapper);
        }

        internal static RoleCommandRepository CreateroleCommandRepositoryInstance(IFoodOrderingContextFactory factory)
        {
            return new RoleCommandRepository(factory, _mapper);
        }

        #endregion Role
    }
}
#pragma warning restore SA1124 // Do not use regions