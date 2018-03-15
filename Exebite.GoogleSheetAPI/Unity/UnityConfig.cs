using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.Kasa;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Unity;

namespace Exebite.GoogleSheetAPI.Unity
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
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
