using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Interfaces;
using Exebite.Sheets.Common.Models;
using Exebite.Sheets.Common.Util;
using Exebite.Sheets.Reader;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Exebite.Sheets.Hedone
{
    public class HedoneReader : IRestaurantReader
    {
        #region private members
        private readonly ILogger _logger;
        private readonly SheetReader _reader; 
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new Hedone sheet reader with provided logger.
        /// This can fail.
        /// </summary>
        /// <param name="logger"></param>
        public HedoneReader(ILogger logger)
        {
            _logger = logger;

            try
            {
                _reader = new SheetReader(_logger, Configuration.HedoneSheetID);
            }
            catch (Exception ex)
            {
                logger.LogError($"Could not instantiate Hedone Sheet Reader, with error {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Interface implementation
        /// <summary>
        /// Reads food items that are offered any day.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FoodItem> ReadFoodItems()
        {
            return DataExtractor.ExtractFoodItems(
                _reader.ReadSheetData(
                    Configuration.HedoneOfferRange));
        }

        /// <summary>
        /// Reads daily offers for provided Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<DailyOfferFood> ReadDailyOffers(DateTime date)
        {
            var foundMerge = FindDateRangeInSheets(date);

            if (foundMerge.IsSuccess)
            {
                var namesRange = A1Notation.ToRangeFormat(
                        foundMerge.Value.Range.StartColumnIndex.Value, 2, // Start Corner
                        foundMerge.Value.Range.EndColumnIndex.Value, 6); // End corner

                var offersList = _reader.ReadSheetData(string.Format("'{0}'!{1}", foundMerge.Value.SheetName, namesRange));

                return DataExtractor.ExtractDailyOffers(offersList);
            }
            else
            {
                _logger.LogError($"Unable to load daily offers for Restaurant Hedone on date {date.Date.ToString()}");
                return new DailyOfferFood[0];
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Returns a list of all merged regions 
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private IEnumerable<MergedRegion> SheetMerges(Sheet sheet)
        {
            if (sheet.Merges == null)
            {
                return new List<MergedRegion>();
            }

            return sheet.Merges.Select(merge => new MergedRegion(sheet, merge));
        }

        /// <summary>
        /// Finds the merge inside all the sheets.
        /// </summary>
        /// <param name="providedDate"></param>
        /// <returns></returns>
        private Result<MergedRegion> FindDateRangeInSheets(DateTime providedDate)
        {
            var foundRegion = _reader.GetWorkSheets()
                .Select(SheetMerges)
                .Select(mergeList => GetMergeWithDate(mergeList, providedDate))
                .Where(result => result.IsSuccess)
                .FirstOrDefault();

            if (foundRegion != null)
            {
                return foundRegion;
            }

            return Result<MergedRegion>.Fail(null, string.Format("No merge with provided date {0}.", providedDate.ToString()));
        }

        /// <summary>
        /// From all the merges, find the one that has provided date.
        /// </summary>
        /// <param name="merges"></param>
        /// <param name="providedDate"></param>
        /// <returns></returns>
        private Result<MergedRegion> GetMergeWithDate(IEnumerable<MergedRegion> merges, DateTime providedDate)
        {
            foreach (var merge in merges)
            {
                var result = _reader.ReadDateTime(merge.A1FirstCell);
                if (result.IsSuccess)
                {
                    DateTime parsedDate = result.Value;

                    if (providedDate.Year != parsedDate.Year
                        || providedDate.Month != parsedDate.Month)
                        break;

                    if (providedDate.Date.Equals(parsedDate.Date))
                        return Result<MergedRegion>.Success(merge);
                }

                // There is a limitation on Google Side for 100 calls per second.
                // We have added this thread sleep to avoid such issues.
                Thread.Sleep(Constants.SLEEP_TIME);
            }

            return Result<MergedRegion>.Fail(null, "Sheet doesn't contain this date.");
        } 
        #endregion

    }
}
