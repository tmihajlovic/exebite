using Exebite.Business;
using Exebite.Business.GoogleApiImportExport;
using Exebite.GoogleSheetAPI;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.Kasa;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using System;
using Unity;

namespace Exebite.JobScheduler.Unity
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Gets configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IJobSchedulerRepository, JobSchedulerRepository>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IFoodService, FoodService>();
            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IRestaurantService, RestaurantService>();
            container.RegisterType<IGoogleDataImporter, GoogleApiImport>();
            container.RegisterType<IGoogleDataExporter, GoogleApiExport>();

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
