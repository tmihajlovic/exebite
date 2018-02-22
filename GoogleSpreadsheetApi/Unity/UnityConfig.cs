using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.RestaurantConectors;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using Unity;

namespace Exebite.GoogleSpreadsheetApi.Unity
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>();
            container.RegisterType<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>();

            container.RegisterType<IRestaurantConector, RestaurantConector>();
            container.RegisterType<IHedoneConector,HedoneConector>();
            container.RegisterType<ILipaConector, LipaConector>();
            container.RegisterType<ITeglasConector, TeglasConector>();

        }
    }
}
