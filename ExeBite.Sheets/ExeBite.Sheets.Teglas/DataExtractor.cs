using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Models;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace Exebite.Sheets.Teglas
{
    public class DataExtractor
    {
        #region Extracting regular food items
        /// <summary>
        /// Used to extract standing food offer for the restaurant.
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static List<FoodItem> ExtractFoodItems(ValueRange ranges)
        {
            var foundFood = new List<FoodItem>();
            var category = new Category();

            foreach (var row in ranges.Values)
            {
                if (row.Count < 3)
                {
                    var (HasCategory, FoundCategory) = TryNewCategory(row);
                    if (HasCategory) { category = FoundCategory; }

                    continue;
                }

                foundFood.Add(
                    ExtractSingle(row, category));
            }

            return foundFood;
        }

        /// <summary>
        /// Extracts single item from provided row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private static FoodItem ExtractSingle(IList<object> row, Category category)
        {
            return new FoodItem(
                    ExtractName(row),           //Name
                    ExtractPrice(row),          //Price
                    Constants.TEGLAS_NAME,      //Restaurant
                    category,                   //Subcategory
                    ExtractDescription(row));   //Description
        }

        /// <summary>
        /// Extracts category if there is one.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static (bool HasCategory, Category FoundCategory) TryNewCategory(IList<object> row)
        {
            bool success = false;
            Category category = new Category();

            if (row.Count > 0)
            {
                var subCategory = row[0].ToString();
                if (!string.IsNullOrEmpty(subCategory))
                {
                    category = new Category(
                        Constants.CATEGORY_STANDARD,
                        subCategory);
                    success = true;

                }
            }
            return (success, category);
        }

        private static string ExtractName(IList<object> row)
        {
            return row[0].ToString();
        }

        private static double ExtractPrice(IList<object> row)
        {
            return double.Parse(row[2].ToString());
        }

        private static string ExtractDescription(IList<object> row)
        {
            return row[1].ToString();
        } 
        #endregion
    }
}
