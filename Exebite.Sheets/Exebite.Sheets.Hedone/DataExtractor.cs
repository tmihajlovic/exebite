using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Models;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace Exebite.Sheets.Hedone
{
    /// <summary>
    /// Used to extract data from Hedone restaurant list.
    /// </summary>
    public class DataExtractor
    {
        #region Statics
        private static readonly Category dailyCategory;
        private static readonly Category standardCategory;

        /// <summary>
        /// Static constructor that initializes static members of the class.
        /// </summary>
        static DataExtractor()
        {
            dailyCategory = new Category(Constants.CATEGORY_DAILY);
            standardCategory = new Category(Constants.CATEGORY_STANDARD);
        }
        #endregion

        #region Daily offer extraction
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
                foundFood.Add(
                    DailyOffer(ranges, i));
            }

            return foundFood;
        }

        /// <summary>
        /// Extracts single daily offer from ValueRange, at position i.
        /// </summary>
        /// <param name="ranges"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static DailyOfferFood DailyOffer(ValueRange ranges, int i)
        {
            return new DailyOfferFood(
                GetDailyStringAt(ranges.Values[0], i),  // Name
                GetDailyDoubleAt(ranges.Values[3], i),  // Price
                Constants.HEDONE_NAME,                  // Restaurant
                dailyCategory);                         // Category
        }

        /// <summary>
        /// Extracts string from position i in a list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static string GetDailyStringAt(IList<object> list, int i)
        {
            return list[i].ToString();
        }

        /// <summary>
        /// Extracts double from position i in a list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static double GetDailyDoubleAt(IList<object> list, int i)
        {
            return double.Parse(
                GetDailyStringAt(list, i));
        }
        #endregion

        #region Standard offer extraction
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

                foundFood.Add(
                    ExtractFoodItem(row));
            }

            return foundFood;
        }

        /// <summary>
        /// Extract food item from the row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static FoodItem ExtractFoodItem(IList<object> row)
        {
            return new FoodItem(
                ExtractFoodName(row),           //Name
                ExtractFoodPrice(row),          //Price
                Constants.HEDONE_NAME,          //Restaurant
                standardCategory,               //Subcategory
                ExtractFoodDescription(row));  //Description
        }

        /// <summary>
        /// Extracts food name from the row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static string ExtractFoodName(IList<object> row)
        {
            return row[0].ToString();
        }

        /// <summary>
        /// Extracts food price from the row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static double ExtractFoodPrice(IList<object> row)
        {
            return double.Parse(row[3].ToString());
        }

        /// <summary>
        /// Extracts food description from the row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static string ExtractFoodDescription(IList<object> row)
        {
            return row[1].ToString();
        } 
        #endregion
    }
}
