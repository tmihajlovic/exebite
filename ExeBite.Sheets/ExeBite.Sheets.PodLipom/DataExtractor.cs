using ExeBite.Sheets.Common;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace ExeBite.Sheets.PodLipom
{
    public class DataExtractor
    {
        private static readonly Category dailyOffer;

        static DataExtractor()
        {
            dailyOffer = new Category(Constants.CATEGORY_DAILY);
        }

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
                var foodName = ranges.Values[0][i].ToString();
                if (!string.IsNullOrWhiteSpace(foodName))
                {
                    foundFood.Add(new DailyOfferFood(
                        foodName,
                        double.Parse(ranges.Values[3][i].ToString()),
                        Constants.POD_LIPOM_NAME,
                        dailyOffer));
                }
            }

            return foundFood;
        }
    }
}
