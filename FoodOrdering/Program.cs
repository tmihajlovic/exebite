using FoodOrdering.Unity;
using Unity;

namespace FoodOrdering
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new UnityContainer())
            {
                // Register type
                UnityConfig.RegisterTypes(container);
                var app = container.Resolve<IApplication>();

                app.Run(args);
            }

        }
    }
}
