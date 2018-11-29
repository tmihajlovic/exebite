using Exebite.Sheets.Common;
using Exebite.Sheets.Hedone;
using Exebite.Sheets.Index;
using Exebite.Sheets.PodLipom;
using Exebite.Sheets.Teglas;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Exebite.Sheets.API
{
    public class SheetsAPI : ISheetsAPI
    {
        private readonly HedoneReader _hedone;
        private readonly IndexReader _index;
        private readonly PodLipomReader _podLipom;
        private readonly TeglasReader _teglas;
        private readonly DummyLogger dummyLogger;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SheetsAPI()
        {
            dummyLogger = new DummyLogger();
            _hedone = new HedoneReader(dummyLogger);
            _index = new IndexReader(dummyLogger);
            _podLipom = new PodLipomReader(dummyLogger);
            _teglas = new TeglasReader(dummyLogger);
        }

        /// <summary>
        /// Returns a list of restaurant offers.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<RestaurantOffer> GetOffers(DateTime date)
        {
            var result = new List<RestaurantOffer>();

            var hedoneDaily = _hedone.ReadDailyOffers(date).ToList();
            var hedoneStandard = _hedone.ReadFoodItems().ToList();
            var hedoneOffer = new RestaurantOffer(date, Constants.HEDONE_NAME, hedoneDaily, hedoneStandard);
            result.Add(hedoneOffer);

            var indexDaily = _index.ReadDailyOffers(date).ToList();
            var indexStandard = _index.ReadFoodItems().ToList();
            var indexOffer = new RestaurantOffer(date, Constants.INDEX_NAME, indexDaily, indexStandard);
            result.Add(indexOffer);

            var podLipomDaily = _podLipom.ReadDailyOffers(date).ToList();
            var podLipomStandard = _podLipom.ReadFoodItems().ToList();
            var podLipomOffer = new RestaurantOffer(date, Constants.POD_LIPOM_NAME, podLipomDaily, podLipomStandard);
            result.Add(podLipomOffer);

            var teglasDaily = _teglas.ReadDailyOffers(date).ToList();
            var teglasStandard = _teglas.ReadFoodItems().ToList();
            var teglasOffer = new RestaurantOffer(date, Constants.TEGLAS_NAME, teglasDaily, teglasStandard);
            result.Add(teglasOffer);

            return result;
        }
    }
}
