using ExeBite.Sheets.Common;
using ExeBite.Sheets.Common.Interfaces;
using ExeBite.Sheets.Reader;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExeBite.Sheets.Index
{
    public class IndexReader : IRestaurantReader
    {
        #region private members
        private readonly ILogger _logger;
        private readonly SheetReader _reader;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new Teglas sheet reader with provided logger.
        /// This can fail.
        /// </summary>
        /// <param name="logger"></param>
        public IndexReader(ILogger logger)
        {
            _logger = logger;

            try
            {
                _reader = new SheetReader(_logger, Configuration.IndexSheetID);
            }
            catch (Exception ex)
            {
                logger.LogError($"Could not instantiate Index House Sheet Reader, with error {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Interface implementation
        /// <summary>
        /// Reads daily offers from Index house. Currently none.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<DailyOfferFood> ReadDailyOffers(DateTime date)
        {
            return new DailyOfferFood[0];
        }

        /// <summary>
        /// Reads food items offered every day by the Index house restaurant.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FoodItem> ReadFoodItems()
        {
            return DataExtractor.ExtractFoodItems(
                _reader.ReadSheetData(
                    Configuration.IndexOfferRange));
        } 
        #endregion
    }
}
