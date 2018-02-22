using Unity;

namespace BusinessTest
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            GoogleSpreadsheetApi.Unity.UnityConfig.RegisterTypes(container);
            Exebite.Business.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
