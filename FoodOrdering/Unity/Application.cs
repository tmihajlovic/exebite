using Exebite.Business;
using Exebite.Model;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.RestaurantConectors;
using System;
using System.Collections.Generic;
using Unity;
using Exebite.Business.GoogleApiImportExport;
using Exebite.GoogleSpreadsheetApi.Strategies;
using System.Linq;

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
            IFoodService foodService = UnityConfig.Container.Resolve<IFoodService>();
            ICustomerService customerService = UnityConfig.Container.Resolve<ICustomerService>();

            LipaConector lipaConector = new LipaConector(GoogleSSFactory, GoogleSSIdFactory);
            TeglasConector teglasConector = new TeglasConector(GoogleSSFactory, GoogleSSIdFactory);
            HedoneConector hedoneConector = new HedoneConector(GoogleSSFactory, GoogleSSIdFactory);
            LipaStrategy lipaStrategy = new LipaStrategy(GoogleSSFactory,GoogleSSIdFactory);

            Restaurant _tegla = restarauntService.GetRestaurantById(4);
            //Customer _customer = customerService.GetCustomerById(85);

            var orders = lipaStrategy.GetHistoricalData();

            var groupedOrders = orders.GroupBy(c => c.Customer);

            foreach(var c in groupedOrders)
            {
                customerService.CreateCustomer(c.Key);
            }



            //var choise = ' ';

            //Console.Clear();
            //Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - - - - - ");
            //Console.WriteLine("Startup - will be done on job @7am");
            //Console.WriteLine("");
            //Console.WriteLine("Update menu");
            //dataImporter.UpdateRestorauntsMenu();

            //_tegla.DailyMenu = teglasConector.GetDailyMenu();
            //_tegla.Foods = teglasConector.LoadAllFoods();
            //restarauntService.UpdateRestourant(_tegla);

            //_tegla = restarauntService.GetRestaurantById(4);
            //Console.WriteLine("Done!");
            //Console.WriteLine("");
            //foreach(var food in _tegla.DailyMenu)
            //{
            //    Console.WriteLine(food.Name);
            //}
            //Console.WriteLine("");
            //Console.WriteLine("Place oreder");



            //while (choise != 'q')
            //{



            //    Console.WriteLine("1 - " + _tegla.DailyMenu[0].Name);
            //    Console.WriteLine("2 - " + _tegla.DailyMenu[1].Name);
            //    Console.WriteLine("3 - " + _tegla.DailyMenu[2].Name);
            //    Console.WriteLine("4 - " + _tegla.DailyMenu[3].Name);
            //    Console.WriteLine("5 - " + _tegla.DailyMenu[4].Name);
            //    Console.WriteLine("6 - " + _tegla.DailyMenu[5].Name);
            //    Console.WriteLine("7 - ");
            //    Console.WriteLine("8 - Check orders");
            //    Console.WriteLine("9 - Write orders to sheet");
            //    Console.WriteLine("q or other - quit");

            //    Order newOrder = new Order();
            //    choise = Console.ReadKey().KeyChar;
            //    string _note = Console.ReadLine();
            //    switch (choise)
            //    {
            //        case '1':
            //            newOrder = new Order
            //            {
            //                Customer = _customer,
            //                Date = DateTime.Today,
            //                Meal = new Meal
            //                {
            //                    Foods = new List<Food>
            //                     {
            //                         _tegla.DailyMenu[0]
            //                     },
            //                    Price = _tegla.DailyMenu[0].Price
            //                },
            //                Price = _tegla.DailyMenu[0].Price


            //            };
            //            Console.WriteLine("Napisite napomenu");
            //            _note = Console.ReadLine();
            //            newOrder.Note = _note;
            //            orderService.PlaceOreder(newOrder);
            //            break;

            //        case '2':
            //            newOrder = new Order
            //            {
            //                Customer = _customer,
            //                Date = DateTime.Today,
            //                Meal = new Meal
            //                {
            //                    Foods = new List<Food>
            //                     {
            //                         _tegla.DailyMenu[1]
            //                     },
            //                    Price = _tegla.DailyMenu[1].Price
            //                },
            //                Price = _tegla.DailyMenu[1].Price


            //            };
            //            Console.WriteLine("Napisite napomenu");
            //            _note = Console.ReadLine();
            //            newOrder.Note = _note;
            //            orderService.PlaceOreder(newOrder);
            //            break;

            //        case '3':
            //            newOrder = new Order
            //            {
            //                Customer = _customer,
            //                Date = DateTime.Today,
            //                Meal = new Meal
            //                {
            //                    Foods = new List<Food>
            //                     {
            //                         _tegla.DailyMenu[2]
            //                     },
            //                    Price = _tegla.DailyMenu[2].Price
            //                },
            //                Price = _tegla.DailyMenu[2].Price


            //            };
            //            Console.WriteLine("Napisite napomenu");
            //            _note = Console.ReadLine();
            //            newOrder.Note = _note;
            //            orderService.PlaceOreder(newOrder);
            //            break;

            //        case '4':
            //            newOrder = new Order
            //            {
            //                Customer = _customer,
            //                Date = DateTime.Today,
            //                Meal = new Meal
            //                {
            //                    Foods = new List<Food>
            //                     {
            //                         _tegla.DailyMenu[3]
            //                     },
            //                    Price = _tegla.DailyMenu[3].Price
            //                },
            //                Price = _tegla.DailyMenu[3].Price


            //            };
            //            Console.WriteLine("Napisite napomenu");
            //            _note = Console.ReadLine();
            //            newOrder.Note = _note;
            //            orderService.PlaceOreder(newOrder);
            //            break;

            //        case '5':
            //            newOrder = new Order
            //            {
            //                Customer = _customer,
            //                Date = DateTime.Today,
            //                Meal = new Meal
            //                {
            //                    Foods = new List<Food>
            //                     {
            //                         _tegla.DailyMenu[4]
            //                     },
            //                    Price = _tegla.DailyMenu[4].Price
            //                },
            //                Price = _tegla.DailyMenu[4].Price


            //            };
            //            Console.WriteLine("Napisite napomenu");
            //            _note = Console.ReadLine();
            //            newOrder.Note = _note;
            //            orderService.PlaceOreder(newOrder);
            //            break;

            //        case '6':
            //            newOrder = new Order
            //            {
            //                Customer = _customer,
            //                Date = DateTime.Today,
            //                Meal = new Meal
            //                {
            //                    Foods = new List<Food>
            //                     {
            //                         _tegla.DailyMenu[5]
            //                     },
            //                    Price = _tegla.DailyMenu[5].Price
            //                },
            //                Price = _tegla.DailyMenu[5].Price


            //            };
            //            Console.WriteLine("Napisite napomenu");
            //            _note = Console.ReadLine();
            //            newOrder.Note = _note;
            //            orderService.PlaceOreder(newOrder);
            //            break;

            //        case '7':
            //            break;

            //        case '8':
            //            var todayOrders = orderService.GettOrdersForDate(DateTime.Today);
            //            foreach (var order in todayOrders)
            //            {
            //                Console.WriteLine("------------------------------------------");
            //                Console.WriteLine("");
            //                Console.WriteLine("Narucio: " + order.Customer.Name);
            //                Console.WriteLine("Cena:" + order.Price.ToString());
            //                Console.WriteLine("Narucena hrana:");
            //                foreach (var food in order.Meal.Foods)
            //                {
            //                    Console.WriteLine(" -" + food.Name);
            //                    Console.WriteLine("");
            //                }
            //                Console.WriteLine("------------------------------------------");
            //            }


            //            break;

            //        case '9':
            //            todayOrders = orderService.GettOrdersForDate(DateTime.Today);
            //            teglasConector.PlaceOrders(todayOrders);
            //            break;

            //        case 'q':
            //            break;

            //    }

            //}
        }
    }
}
