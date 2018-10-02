using ExeBite.Sheets.Common;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace ExeBite.Sheets.Hedone
{
    /// <summary>
    /// Used to extract data from Hedone restaurant list.
    /// </summary>
    public class DataExtractor
    {
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
                foundFood.Add(new DailyOfferFood(
                    ranges.Values[0][i].ToString(),                 //Name
                    double.Parse(ranges.Values[3][i].ToString()),   //Price
                    Constants.HEDONE_NAME));                        //Restaurant
            }

            return foundFood;
        }

        /// <summary>
        /// Used to extract standing food offer for the restaurant.
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static List<FoodItem> ExtractFoodItems(ValueRange ranges)
        {
            var foundFood = new List<FoodItem>();
            foreach (var row in ranges.Values)
            {
                if (row.Count < 4)
                {
                    continue;
                }

                foundFood.Add(new FoodItem(
                    row[0].ToString(),                  //Name
                    double.Parse(row[3].ToString()),    //Price
                    Constants.HEDONE_NAME,              //Restaurant
                    Constants.NO_CATEGORY,              //Subcategory
                    row[1].ToString()));                //Description
            }

            return foundFood;
        }
    }
}
