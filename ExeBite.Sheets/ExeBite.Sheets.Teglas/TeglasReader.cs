using ExeBite.Sheets.Common;
using ExeBite.Sheets.Common.Interfaces;
using ExeBite.Sheets.Reader;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExeBite.Sheets.Teglas
{
    public class TeglasReader : IRestaurantReader
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
        public TeglasReader(ILogger logger)
        {
            _logger = logger;

            try
            {
                _reader = new SheetReader(_logger, Configuration.TeglasSheetID);
            }
            catch (Exception ex)
            {
                logger.LogError($"Could not instantiate Teglas Sheet Reader, with error {ex.Message}");
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Read daily offers from Teglas.
        /// Currently none.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<DailyOfferFood> ReadDailyOffers(DateTime date)
        {
            return new DailyOfferFood[0];
        }

        /// <summary>
        /// Reads food items from Teglas restaurant.
        /// All food items in Teglas are in this category.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FoodItem> ReadFoodItems()
        {
            return DataExtractor.ExtractFoodItems(
                _reader.ReadSheetData(
                    Configuration.TeglasOfferRange));
        }
        
    }
}
