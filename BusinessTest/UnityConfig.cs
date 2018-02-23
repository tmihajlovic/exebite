using Unity;

namespace BusinessTest
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            Exebite.GoogleSpreadsheetApi.Unity.UnityConfig.RegisterTypes(container);
            Exebite.Business.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
