using Exebite.Sheets.Common;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace Exebite.Sheets.PodLipom
{
    public class DataExtractor
    {
        #region Static members and constructors
        private static readonly Category dailyOffer;

        /// <summary>
        /// Static extractor used to initialize static members.
        /// </summary>
        static DataExtractor()
        {
            dailyOffer = new Category(Constants.CATEGORY_DAILY);
        }
        #endregion

        #region Daily offers extraction
        /// <summary>
        /// Used to extract daily offers in specific range.
        /// Uses a lot of magic numbers.
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static List<DailyOfferFood> ExtractDailyOffers(ValueRange ranges)
        {
            var length = ranges.Values[0].Count;
            var foundFood = new List<DailyOfferFood>();

            for (int i = 0; i < length; i++)
            {
                foundFood.AddRange(
                    SingleOffer(ranges.Values, i));


            }

            return foundFood;
        }

        /// <summary>
        /// Extracts single offer from offers matrix, one at position i.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static List<DailyOfferFood> SingleOffer(IList<IList<object>> matrix, int i)
        {
            var result = new List<DailyOfferFood>();

            var (HasName, Name) = TryExtractName(matrix[0], i);
            if (HasName)
            {
                result.Add(
                    new DailyOfferFood(
                        Name,                           //Name
                        ExtractDouble(matrix[3], i),    //Price
                        Constants.POD_LIPOM_NAME,       //Restaurant    
                        dailyOffer));                   //Categiory
            }

            return result;
        }

        /// <summary>
        /// Extracts food name if there exists one.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static (bool HasName, string Name) TryExtractName(IList<object> row, int i)
        {
            var name = row[i].ToString();
            var hasName = !string.IsNullOrWhiteSpace(name);
            return (hasName, name);
        }

        /// <summary>
        /// Checks if there is string resembling a name on specified location.
        /// Returns name as out parameter.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="i"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryExtractName2(IList<object> row, int i, out string result)
        {
            result = row[i].ToString();

            return !string.IsNullOrWhiteSpace(result);
        }

        /// <summary>
        /// Extracts double value from specified location.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static double ExtractDouble(IList<object> row, int i)
        {
            return double.Parse(row[i].ToString());
        }
    } 
    #endregion
}
