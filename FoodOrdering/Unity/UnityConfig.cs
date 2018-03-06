using System;
using Unity;

namespace FoodOrdering.Unity
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IApplication, Application>();
            Exebite.DataAccess.Unity.UnityConfig.RegisterTypes(container);
            Exebite.Business.Unity.UnityConfig.RegisterTypes(container);
            Exebite.GoogleSpreadsheetApi.Unity.UnityConfig.RegisterTypes(container);
            Exebite.JobScheduler.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
