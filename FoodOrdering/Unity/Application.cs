using Exebite.Business;
using Exebite.Model;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.RestaurantConectors;
using System;
using Unity;
using Exebite.Business.GoogleApiImportExport;
using Exebite.GoogleSpreadsheetApi.Strategies;
using Exebite.JobScheduler;

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
            IJobSchedulerRepository jobSchedulerRepository = UnityConfig.Container.Resolve<IJobSchedulerRepository>();

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
                Console.WriteLine("3 - Write orders to sheet thru scheduler");
                Console.WriteLine("q - quit");
                choise = Console.ReadKey().KeyChar;

                switch (choise)
                {
                    case '1':
                        //var dailyMenuLipa = lipaStrategy.GetDailyMenu();
                        //_lipa.DailyMenu.Clear();
                        //_lipa.DailyMenu.AddRange(dailyMenuLipa);
                        //restarauntService.UpdateRestourant(_lipa);
                        oldSheets.UpdateDailyMenu();
                        break;

                    case '2':
                        //var orders = orderService.GetAllOrdersForRestoraunt(1).Where(o => o.Date == DateTime.Today).ToList();
                        //lipaStrategy.PlaceOrders(orders);
                        oldSheets.WriteOrdersToSheets();
                        break;

                    case '3':
                        jobSchedulerRepository.RemoveAllData();
                        jobSchedulerRepository.RegisterJobsToDB();
                        var cron = "0 0/1 * 1/1 * ? *";
                        jobSchedulerRepository.ScheduleJobCronExpresion("WriteOrders", "GoogleSheets", cron, "TestWrite");

                        break;
                    case 'q':
                        loopBreak = true;
                        break;
                }
            }

        }
    }

}
