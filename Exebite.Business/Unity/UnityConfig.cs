using Unity;

namespace Exebite.Business.Unity
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IFoodService, FoodService>();
            container.RegisterType<ILocationService, LocationService>();
            container.RegisterType<IRestarauntService, RestarauntService>();
            container.RegisterType<IGoogleDataImporter, GoogleApiImport>();
            container.RegisterType<IGoogleDateExporter, GoogleApiExport>();
        }
    }
}
