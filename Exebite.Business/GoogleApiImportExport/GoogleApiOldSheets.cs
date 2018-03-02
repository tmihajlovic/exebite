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
        private IRestarauntService _restarauntService;
        private IRestaurantStrategy _lipa;
        private IRestaurantStrategy _hedone;
        private IRestaurantStrategy _indexHouse;
        private IRestaurantStrategy _teglas;
        private IRestaurantStrategy _extraFood;

        public GoogleApiOldSheets(IGoogleSheetServiceFactory googleSheetServiceFactory, IGoogleSpreadsheetIdFactory googleSpreadsheetIdFactory, IRestarauntService restarauntService)
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

        public void UpdateDailyMenu()
        {
            Restaurant lipaRestoraunt = _restarauntService.GetRestaurantByName("Restoran pod Lipom");
            Restaurant hedoneRestoraunt = _restarauntService.GetRestaurantByName("Hedone");
            Restaurant indexHauseRestoraunt = _restarauntService.GetRestaurantByName("Index House");
            Restaurant teglasRestoraunt = _restarauntService.GetRestaurantByName("Teglas");
            Restaurant extraFoodRestoraunt = _restarauntService.GetRestaurantByName("Extra food");

            // Get daily menu and update info in database
            //Lipa
            lipaRestoraunt.DailyMenu = _lipa.GetDailyMenu();
            _restarauntService.UpdateRestourant(lipaRestoraunt);
            //Teglas
            teglasRestoraunt.DailyMenu = _teglas.GetDailyMenu();
            _restarauntService.UpdateRestourant(teglasRestoraunt);
            //Hedone
            hedoneRestoraunt.DailyMenu = _hedone.GetDailyMenu();
            _restarauntService.UpdateRestourant(hedoneRestoraunt);
            //Index house
            indexHauseRestoraunt.DailyMenu = _indexHouse.GetDailyMenu();
            _restarauntService.UpdateRestourant(indexHauseRestoraunt);
            // Extra food
            extraFoodRestoraunt.DailyMenu = _extraFood.GetDailyMenu();
            _restarauntService.UpdateRestourant(extraFoodRestoraunt);
        }

        public void WriteOrdersToSheets(List<Order> orders)
        {
            throw new NotImplementedException();
        }
    }
}
