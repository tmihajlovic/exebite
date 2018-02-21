using Exebite.Business;
using Exebite.Model;
using GoogleSpreadsheetApi.GoogleSSFactory;
using GoogleSpreadsheetApi.RestaurantHandler;
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
            IGoogleSheetServiceFactory GoogleSSFactory = UnityConfig.Container.Resolve<IGoogleSheetServiceFactory>();
            IGoogleSpreadsheetIdFactory GoogleSSIdFactory = UnityConfig.Container.Resolve<IGoogleSpreadsheetIdFactory>();


            Console.WriteLine("1 - get historical data");
            Console.WriteLine("2 - Update daily menu");
            Console.WriteLine("3 - Full Update");
            Console.WriteLine("q or other - quit");

            var choise = Console.ReadKey().KeyChar;

            switch(choise)
            {
                case '1':
                    //historical data import
                    Console.WriteLine("Import all data");
                    var data = dataImporter.GetHistoricalData();
                    foreach (var order in data)
                    {
                        orderService.PlaceOreder(order);
                    }
                    Console.WriteLine("Done!");
                    Console.ReadLine();
                    break;

                case '2':
                    //daily menu
                    Console.WriteLine("DailyMenuUpdate");
                    Console.WriteLine("");
                    dataImporter.UpdateRestorauntsMenu();
                    var restauratns = restarauntService.GetAllRestaurants();
                    foreach (var restaurant in restauratns)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine(restaurant.Name);
                        foreach (var food in restaurant.DailyMenu)
                        {
                            Console.WriteLine("*" + food.Name + " " + "-" + " " + food.Price);
                        }
                    }
                    Console.ReadLine();
                    break;

                case '3':
                    //historical data import
                    Console.WriteLine("Import historical data");
                    data = dataImporter.GetHistoricalData();
                    foreach (var order in data)
                    {
                        orderService.PlaceOreder(order);
                    }
                    Console.WriteLine("");
                    //update daily
                    Console.WriteLine("DailyMenuUpdate");
                    dataImporter.UpdateRestorauntsMenu();
                    Console.WriteLine("Done!");
                    Console.ReadLine();
                    break;

                case '5':
                    //test
                    Console.WriteLine("");
                    Console.WriteLine("------------------Test get daily-----------------------");

                    LipaHandler lh = new LipaHandler(GoogleSSFactory, GoogleSSIdFactory);
                    var result = lh.GetDalyMenu();

                    break;

                case '6':
                    //test
                    Console.WriteLine("");
                    Console.WriteLine("------------------Test place order-----------------------");

                    LipaHandler lhp = new LipaHandler(GoogleSSFactory, GoogleSSIdFactory);
                    List<Order> orderList = new List<Order>();
                    orderList = orderService.GettOrdersForDate(new DateTime(2017,11,06));
                    lhp.PlaceOrders(orderList);

                    break;

                case '7':
                    //test
                    Console.WriteLine("");
                    Console.WriteLine("------------------Write menu-----------------------");

                    lh = new LipaHandler(GoogleSSFactory, GoogleSSIdFactory);
                    List<Food> foodList = new List<Food>();
                    Restaurant restLispa = restarauntService.GetRestaurantById(1);
                    lh.WriteMenu(restLispa.Foods);

                    break;

                case '9':
                    //test
                    Console.WriteLine("");
                    Console.WriteLine("------------------setup menu-----------------------");

                    lh = new LipaHandler(GoogleSSFactory, GoogleSSIdFactory);
                    lh.DnevniMenuSheetSetup();

                    break;


                case 'q':
                    break;

            }
        }
    }
}
