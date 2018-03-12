using Unity;

namespace Exebite.Business.Test.Unity
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            Business.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
