using Unity;

namespace GoogleSpreadsheetApi.Test
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            Exebite.GoogleSpreadsheetApi.Unity.UnityConfig.RegisterTypes(container);
            Exebite.DataAccess.Unity.UnityConfig.RegisterTypes(container);
            Exebite.Business.Unity.UnityConfig.RegisterTypes(container);
            Exebite.GoogleSpreadsheetApi.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
