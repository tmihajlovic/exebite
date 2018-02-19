using Exebite.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FoodOrdering.Unity
{
    class Application : IApplication
    {
        public void Run(string[] args)
        {
            IGoogleDataImporter dataImporter = UnityConfig.Container.Resolve<IGoogleDataImporter>();
            IOrderService orderService = UnityConfig.Container.Resolve<IOrderService>();
            IRestarauntService restarauntService = UnityConfig.Container.Resolve<IRestarauntService>();

            //historical data import

            //Console.WriteLine("Import all data");
            //var data = dataImporter.GetHistoricalData();
            //foreach (var order in data)
            //{
            //    orderService.PlaceOreder(order);
            //}
            //Console.WriteLine("Done!");

            //daily menu

            Console.WriteLine("DailyMenuUpdate");
            Console.WriteLine("");
            dataImporter.UpdateRestorauntsMenu();
            var restauratns = restarauntService.GetAllRestaurants();
            foreach (var restaurant in restauratns)
            {

                Console.WriteLine(restaurant.Name);
                foreach (var food in restaurant.DailyMenu)
                {
                    Console.WriteLine(food.Name);
                }
            }

            Console.ReadLine();
        }
    }
}
