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
            IGoogleApiOldSheets oldSheets = UnityConfig.Container.Resolve<IGoogleApiOldSheets>();

            LipaConector lipaConector = new LipaConector(GoogleSSFactory, GoogleSSIdFactory);
            TeglasConector teglasConector = new TeglasConector(GoogleSSFactory, GoogleSSIdFactory);
            HedoneConector hedoneConector = new HedoneConector(GoogleSSFactory, GoogleSSIdFactory);

            LipaStrategy lipaStrategy = new LipaStrategy(GoogleSSFactory, GoogleSSIdFactory);

            Restaurant _tegla = restarauntService.GetRestaurantById(4);
            Restaurant _lipa = restarauntService.GetRestaurantById(1);

            //var orders = lipaStrategy.GetHistoricalData();
            

            bool loopBreak = false;
            var choise = '0';
            while (!loopBreak)
            {
                Console.WriteLine("Job to do:");
                Console.WriteLine("1 - Update daily menu");
                Console.WriteLine("2 - Write orders to sheet");
                Console.WriteLine("q - quit");
                choise = Console.ReadKey().KeyChar;

                switch (choise)
                {
                    case '1':
                        var dailyMenuLipa = lipaStrategy.GetDailyMenu();
                        _lipa.DailyMenu.Clear();
                        _lipa.DailyMenu.AddRange(dailyMenuLipa);
                        restarauntService.UpdateRestourant(_lipa);
                        break;

                    case '2':
                        var orders = orderService.GetAllOrdersForRestoraunt(1).Where(o => o.Date == DateTime.Today).ToList();
                        lipaStrategy.PlaceOrders(orders);
                        break;

                    case '3':
                        var oldOrders = oldSheets.GetHistoricalData();
                        foreach(var order in oldOrders)
                        {
                            orderService.PlaceOreder(order);
                        }
                        break;
                    case 'q':
                        loopBreak = true;
                        break;
                }
            }

        }
    }

}
