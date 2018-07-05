using System;
using Exebite.Business;
using Exebite.Business.GoogleApiImportExport;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.Kasa;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
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
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<ICustomerQueryRepository, CustomerQueryRepository>();
            container.RegisterType<ICustomerCommandRepository, CustomerCommandRepository>();
            container.RegisterType<IFoodCommandRepository, FoodCommandRepository>();
            container.RegisterType<IFoodQueryRepository, FoodQueryRepository>();
            container.RegisterType<ILocationCommandRepository, LocationCommandRepository>();
            container.RegisterType<ILocationQueryRepository, LocationQueryRepository>();
            container.RegisterType<IRestaurantCommandRepository, RestaurantCommandRepository>();
            container.RegisterType<IRestaurantQueryRepository, RestaurantQueryRepository>();

            container.RegisterType<IGoogleDataImporter, GoogleApiImport>();
            container.RegisterType<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>();
            container.RegisterType<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>();
            container.RegisterType<IGoogleSheetService, GoogleSheetService>();

            container.RegisterType<IRestaurantConector, RestaurantConector>();
            container.RegisterType<IHedoneConector, HedoneConector>();
            container.RegisterType<ILipaConector, LipaConector>();
            container.RegisterType<ITeglasConector, TeglasConector>();
            container.RegisterType<IKasaConector, KasaConector>();
        }
    }
}
