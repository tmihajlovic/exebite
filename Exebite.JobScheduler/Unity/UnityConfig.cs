﻿using System;
using Exebite.Business;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI.Connectors.Kasa;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;
using Unity;

namespace Exebite.JobScheduler.Unity
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> ContainerValue =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Gets configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => ContainerValue.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IJobSchedulerRepository, JobSchedulerRepository>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IOrderQueryRepository, OrderQueryRepository>();
            container.RegisterType<IOrderCommandRepository, OrderCommandRepository>();
            container.RegisterType<ICustomerQueryRepository, CustomerQueryRepository>();
            container.RegisterType<ICustomerCommandRepository, CustomerCommandRepository>();
            container.RegisterType<IMealCommandRepository, MealCommandRepository>();
            container.RegisterType<IMealQueryRepository, MealQueryRepository>();
            container.RegisterType<ILocationCommandRepository, LocationCommandRepository>();
            container.RegisterType<ILocationQueryRepository, LocationQueryRepository>();
            container.RegisterType<IRestaurantCommandRepository, RestaurantCommandRepository>();
            container.RegisterType<IRestaurantQueryRepository, RestaurantQueryRepository>();
            container.RegisterType<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>();
            container.RegisterType<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>();
            container.RegisterType<IGoogleSheetExtractor, GoogleSheetExtractor>();

            container.RegisterType<IKasaConnector, KasaConnector>();
        }
    }
}
