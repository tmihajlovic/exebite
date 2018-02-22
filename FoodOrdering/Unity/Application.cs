using Exebite.Business;
using Exebite.Model;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.RestaurantConectors;
using System;
using System.Collections.Generic;
using Unity;

namespace FoodOrdering.Unity
{
    class Application : IApplication
    {
        public void Run(string[] args)
        {
            var choise = ' ';

            while (choise != 'q')
            {

                IGoogleDataImporter dataImporter = UnityConfig.Container.Resolve<IGoogleDataImporter>();
                IOrderService orderService = UnityConfig.Container.Resolve<IOrderService>();
                IRestarauntService restarauntService = UnityConfig.Container.Resolve<IRestarauntService>();
                IGoogleSheetServiceFactory GoogleSSFactory = UnityConfig.Container.Resolve<IGoogleSheetServiceFactory>();
                IGoogleSpreadsheetIdFactory GoogleSSIdFactory = UnityConfig.Container.Resolve<IGoogleSpreadsheetIdFactory>();
                IFoodService foodService = UnityConfig.Container.Resolve<IFoodService>();

                LipaConector lipaConector = new LipaConector(GoogleSSFactory, GoogleSSIdFactory);
                TeglasConector teglasConector = new TeglasConector(GoogleSSFactory, GoogleSSIdFactory);
                HedoneConector hedoneConector = new HedoneConector(GoogleSSFactory, GoogleSSIdFactory);


                Console.WriteLine("1 - get historical data");
                Console.WriteLine("2 - Update daily menu");
                Console.WriteLine("3 - Full Update");
                Console.WriteLine("4 - Import food from new");
                Console.WriteLine("5 - Test get daily");
                Console.WriteLine("6 - Test place order -all");
                Console.WriteLine("7 - Write menu");
                Console.WriteLine("8 - ----");
                Console.WriteLine("9 - setup menu");
                Console.WriteLine("q or other - quit");

                choise = Console.ReadKey().KeyChar;
                switch (choise)
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

                    case '4':
                        Console.WriteLine("");
                        Console.WriteLine("Import food from new");
                        Console.WriteLine("");
                        var foods = teglasConector.LoadAllFoods();
                        var teglaRest = restarauntService.GetRestaurantById(4);
                        teglaRest.Foods = foods;
                        restarauntService.UpdateRestourant(teglaRest);

                        break;

                    case '5':
                        //test
                        Console.WriteLine("");
                        Console.WriteLine("------------------Test get daily-----------------------");

                        var resultLipa = lipaConector.GetDalyMenu();
                        var resultHedone = hedoneConector.GetDalyMenu();
                        var resultTegla = teglasConector.GetDalyMenu();

                        break;

                    case '6':
                        //test
                        Console.WriteLine("");
                        Console.WriteLine("------------------Test place order-----------------------");

                        List<Order> orderList = new List<Order>();
                        orderList = orderService.GettOrdersForDate(new DateTime(2017, 11, 06));
                        lipaConector.PlaceOrders(orderList);
                        teglasConector.PlaceOrders(orderList);
                        hedoneConector.PlaceOrders(orderList);
                        break;

                    case '7':
                        //test
                        Console.WriteLine("");
                        Console.WriteLine("------------------Write menu-----------------------");

                        List<Food> foodList = new List<Food>();
                        Restaurant restLispa = restarauntService.GetRestaurantById(1);
                        lipaConector.WriteMenu(restLispa.Foods);
                        Restaurant restTegla = restarauntService.GetRestaurantById(4);
                        teglasConector.WriteMenu(restTegla.Foods);
                        Restaurant restHedone = restarauntService.GetRestaurantById(2);
                        hedoneConector.WriteMenu(restHedone.Foods);

                        break;

                    case '9':
                        //test
                        Console.WriteLine("");
                        Console.WriteLine("------------------setup menu-----------------------");

                        lipaConector.DnevniMenuSheetSetup();
                        hedoneConector.DnevniMenuSheetSetup();

                        break;


                    case 'q':
                        break;

                }

            }
        }
    }
}
