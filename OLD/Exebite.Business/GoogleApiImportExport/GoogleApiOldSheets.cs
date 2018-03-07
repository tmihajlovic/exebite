using Exebite.GoogleSpreadsheetApi;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.Strategies;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiOldSheets : IGoogleApiOldSheets
    {

        IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        IGoogleSheetServiceFactory _googleSheetServiceFactory;
        IOrderService _orderService;
        private IRestarauntService _restaurantService;
        private IRestaurantStrategy _lipaStrategy;
        //private IRestaurantStrategy _hedoneStrategy;
        //private IRestaurantStrategy _indexHouseStrategy;
        //private IRestaurantStrategy _teglasStrategy;
        //private IRestaurantStrategy _extraFoodStrategy;

        public GoogleApiOldSheets(IGoogleSheetServiceFactory googleSheetServiceFactory, IGoogleSpreadsheetIdFactory googleSpreadsheetIdFactory, IRestarauntService restarauntService, IOrderService orderService)
        {
            _googleSpreadsheetIdFactory = googleSpreadsheetIdFactory;
            _googleSheetServiceFactory = googleSheetServiceFactory;
            _restaurantService = restarauntService;
            _orderService = orderService;

            _lipaStrategy = new LipaStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            //_hedoneStrategy = new HedoneStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            //_indexHouseStrategy = new IndexHouseStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            //_teglasStrategy = new TeglasStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            //_extraFoodStrategy = new ExtraFoodStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
        }
        /// <summary>
        /// Gets all oreger that are in spreadsheets
        /// </summary>
        /// <returns>List of all orders</returns>
        public List<Order> GetHistoricalData()
        {
            List<Order> historicalData = new List<Order>();
            historicalData.AddRange(_lipaStrategy.GetHistoricalData());
            //historicalData.AddRange(_hedoneStrategy.GetHistoricalData());
            //historicalData.AddRange(_indexHouseStrategy.GetHistoricalData());
            //historicalData.AddRange(_teglasStrategy.GetHistoricalData());
            //historicalData.AddRange(_extraFoodStrategy.GetHistoricalData());

            return historicalData;
        }

        public void UpdateDailyMenu()
        {
            Restaurant lipaRestoraunt = _restaurantService.GetRestaurantByName("Restoran pod Lipom");
            Restaurant hedoneRestoraunt = _restaurantService.GetRestaurantByName("Hedone");
            Restaurant indexHauseRestoraunt = _restaurantService.GetRestaurantByName("Index House");
            Restaurant teglasRestoraunt = _restaurantService.GetRestaurantByName("Teglas");
            Restaurant extraFoodRestoraunt = _restaurantService.GetRestaurantByName("Extra food");

            // Get daily menu and update info in database
            //Lipa
            lipaRestoraunt.DailyMenu = _lipaStrategy.GetDailyMenu();
            _restaurantService.UpdateRestourant(lipaRestoraunt);

            //TODO implement and uncoment

            ////Teglas
            //teglasRestoraunt.DailyMenu = _teglas.GetDailyMenu();
            //_restarauntService.UpdateRestourant(teglasRestoraunt);
            ////Hedone
            //hedoneRestoraunt.DailyMenu = _hedone.GetDailyMenu();
            //_restarauntService.UpdateRestourant(hedoneRestoraunt);
            ////Index house
            //indexHauseRestoraunt.DailyMenu = _indexHouse.GetDailyMenu();
            //_restarauntService.UpdateRestourant(indexHauseRestoraunt);
            //// Extra food
            //extraFoodRestoraunt.DailyMenu = _extraFood.GetDailyMenu();
            //_restarauntService.UpdateRestourant(extraFoodRestoraunt);
        }

        public void WriteOrdersToSheets()
        {
            var orders = _orderService.GettOrdersForDate(DateTime.Today);

            Restaurant lipaRestaraunt = _restaurantService.GetRestaurantByName("Restoran pod Lipom");
            Restaurant hedoneRestoraunt = _restaurantService.GetRestaurantByName("Hedone");
            Restaurant indexHauseRestoraunt = _restaurantService.GetRestaurantByName("Index House");
            Restaurant teglasRestoraunt = _restaurantService.GetRestaurantByName("Teglas");
            Restaurant extraFoodRestoraunt = _restaurantService.GetRestaurantByName("Extra food");

            

            List<Order> lipaOrders = orders.Where(o => o.Meal.Foods.FirstOrDefault().Restaurant.Id == lipaRestaraunt.Id).ToList();
            _lipaStrategy.PlaceOrders(lipaOrders);
            
            //List<Order> hedoneOrders = orders.Where(o => o.Meal.Foods[0].Restaurant.Id == hedoneRestoraunt.Id).ToList();
            //_hedoneStrategy.PlaceOrders(hedoneOrders);

            //List<Order> teglasOrder = orders.Where(o => o.Meal.Foods[0].Restaurant.Id == teglasRestoraunt.Id).ToList();
            //_teglasStrategy.PlaceOrders(teglasOrder);

            //List<Order> indexHouseOrders = orders.Where(o => o.Meal.Foods[0].Restaurant.Id == indexHauseRestoraunt.Id).ToList();
            //_indexHouseStrategy.PlaceOrders(indexHouseOrders);

            //List<Order> extraFoodOrders = orders.Where(o => o.Meal.Foods[0].Restaurant.Id == extraFoodRestoraunt.Id).ToList();
            //_extraFoodStrategy.PlaceOrders(extraFoodOrders);
        }
    }
}
