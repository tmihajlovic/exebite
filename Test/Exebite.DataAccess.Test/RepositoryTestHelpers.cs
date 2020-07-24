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
        internal static CustomerQueryRepository CreateOnlyCustomerQueryRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new CustomerQueryRepository(factory, _mapper);
        }

        internal static CustomerCommandRepository CreateOnlyCustomerCommandRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new CustomerCommandRepository(factory);
        }
        #endregion

        #region Location
        internal static LocationQueryRepository CreateOnlyLocationQueryRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new LocationQueryRepository(factory, _mapper);
        }

        internal static LocationCommandRepository CreateOnlyLocationCommandRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new LocationCommandRepository(factory, _mapper);
        }

        #endregion Location

        #region Food
        internal static IMealQueryRepository CreateOnlyFoodRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new MealQueryRepository(factory, _mapper);
        }
        #endregion Food

        #region Restaurant
        internal static RestaurantQueryRepository CreateOnlyRestaurantQueryRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new RestaurantQueryRepository(factory, _mapper);
        }

        internal static RestaurantCommandRepository CreateOnlyRestaurantCommandRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new RestaurantCommandRepository(factory, _mapper);
        }

        #endregion

        #region DailyMenu
        internal static DailyMenuQueryRepository CreateDailyMenuQueryRepositoryInstance(IMealOrderingContextFactory factory)
        {
            return new DailyMenuQueryRepository(factory, _mapper);
        }

        internal static DailyMenuCommandRepository CreateDailyMenuCommandRepositoryInstance(IMealOrderingContextFactory factory)
        {
            return new DailyMenuCommandRepository(factory);
        }

        #endregion DailyMenu

        #region Meal
        internal static MealQueryRepository CreateMealQueryRepositoryInstance(IMealOrderingContextFactory factory)
        {
            return new MealQueryRepository(factory, _mapper);
        }

        internal static MealCommandRepository CreateMealCommandRepositoryInstance(IMealOrderingContextFactory factory)
        {
            return new MealCommandRepository(factory);
        }
        #endregion

        #region Payment

        internal static PaymentQueryRepository CreateOnlyPaymentQueryRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new PaymentQueryRepository(factory, _mapper);
        }

        internal static PaymentCommandRepository CreateOnlyPaymentCommandRepositoryInstanceNoData(IMealOrderingContextFactory factory)
        {
            return new PaymentCommandRepository(factory, _mapper);
        }
        #endregion Payment

        #region Order
        internal static OrderCommandRepository CreateOrderCommandRepositoryInstance(IMealOrderingContextFactory factory)
        {
            return new OrderCommandRepository(factory);
        }

        internal static OrderQueryRepository CreateOrderQueryRepositoryInstance(IMealOrderingContextFactory factory)
        {
            return new OrderQueryRepository(factory, _mapper);
        }
        #endregion Order
    }
}
#pragma warning restore SA1124 // Do not use regions