using System.Collections.Generic;
using Exebite.Model;
using GoogleSpreadsheetApi;
using GoogleSpreadsheetApi.GoogleSSFactory;
using GoogleSpreadsheetApi.Strategies;

namespace Exebite.Business
{
    public class GoogleApiImport : IGoogleDataImporter
    {

        IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        IGoogleSheetServiceFactory _googleSheetServiceFactory;
        private IRestarauntService _restarauntService;
        private IRestaurantStrategy _lipa;
        private IRestaurantStrategy _hedone;
        private IRestaurantStrategy _indexHouse;
        private IRestaurantStrategy _teglas;
        private IRestaurantStrategy _extraFood;
        

        public GoogleApiImport(IGoogleSheetServiceFactory googleSheetServiceFactory, IGoogleSpreadsheetIdFactory googleSpreadsheetIdFactory, IRestarauntService restarauntService)
        {
            _googleSpreadsheetIdFactory = googleSpreadsheetIdFactory;
            _googleSheetServiceFactory = googleSheetServiceFactory;
            _restarauntService = restarauntService;
            _lipa = new LipaStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            _hedone = new HedoneStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            _indexHouse = new IndexHouseStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory); 
            _teglas = new TeglasStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
            _extraFood = new ExtraFoodStrategy(googleSheetServiceFactory, googleSpreadsheetIdFactory);
        }

        /// <summary>
        /// Gets all oreger that are in spreadsheets
        /// </summary>
        /// <returns>List of all orders</returns>
        public List<Order> GetHistoricalData()
        {
            List<Order> historicalData = new List<Order>();
            historicalData.AddRange(_lipa.GetHistoricalData());
            historicalData.AddRange(_hedone.GetHistoricalData());
            historicalData.AddRange(_indexHouse.GetHistoricalData());
            historicalData.AddRange(_teglas.GetHistoricalData());
            historicalData.AddRange(_extraFood.GetHistoricalData());

            return historicalData;
        }
        
        /// <summary>
        /// Update daily menu for restorants
        /// </summary>
        public void UpdateRestorauntsMenu()
        {
            Restaurant lipaRestoraunt = _restarauntService.GetRestaurantByName("Restoran pod Lipom");
            Restaurant hedoneRestoraunt = _restarauntService.GetRestaurantByName("Teglas");
            Restaurant indexHauseRestoraunt = _restarauntService.GetRestaurantByName("Index House");
            Restaurant teglasRestoraunt = _restarauntService.GetRestaurantByName("Hedone");
            Restaurant extraFoodRestoraunt = _restarauntService.GetRestaurantByName("Extra food");

            // Get daily menu and update info in database
            lipaRestoraunt.DailyMenu = _lipa.GetDailyMenu();
            _restarauntService.UpdateRestourant(lipaRestoraunt);

            teglasRestoraunt.DailyMenu = _teglas.GetDailyMenu();
            _restarauntService.UpdateRestourant(teglasRestoraunt);

            indexHauseRestoraunt.DailyMenu = _indexHouse.GetDailyMenu();
            _restarauntService.UpdateRestourant(indexHauseRestoraunt);

            hedoneRestoraunt.DailyMenu = _hedone.GetDailyMenu();
            _restarauntService.UpdateRestourant(hedoneRestoraunt);

            extraFoodRestoraunt.DailyMenu = _extraFood.GetDailyMenu();
            _restarauntService.UpdateRestourant(extraFoodRestoraunt);
            
        }
    }
}
