using Unity;

namespace Exebite.DataAccess.Test.Unity
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            DataAccess.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
