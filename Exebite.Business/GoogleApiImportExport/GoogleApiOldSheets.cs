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
    }
}
