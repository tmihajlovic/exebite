
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Customers;
using Exebite.DataAccess.Foods;
using Exebite.DataAccess.Locations;
using Exebite.DataAccess.Meals;
using Exebite.DataAccess.Orders;
using Exebite.DataAccess.Restaurants;
using System;
using Unity;
using Exebite.Business;
using GoogleSpreadsheetApi.GoogleSSFactory;

namespace FoodOrdering.Unity
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IApplication, Application>();
            container.RegisterType<IFoodOrderingContextFactory, FoodOrderingContextFactory>();
            container.RegisterType<ICustomerHandler, CustomerHandler>();
            container.RegisterType<IRestaurantHandler, RestaurantHandler>();
            container.RegisterType<ILocationHandler, LocationHandler>();
            container.RegisterType<IFoodHandler, FoodHandler>();
            container.RegisterType<IMealHandler, MealHandler>();
            container.RegisterType<IOrderHandler, OrderHandler>();
            Exebite.Business.UnityConfig.RegisterTypes(container);
            container.RegisterType<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>();
            container.RegisterType<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>();
        }
    }
}
