using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Models;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;

namespace Exebite.Sheets.Index
{
    public class DataExtractor
    {
        #region Extracting standard offers
        /// <summary>
        /// Used to extract standing food offer for the restaurant.
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static List<FoodItem> ExtractFoodItems(ValueRange ranges)
        {
            var foundFood = new List<FoodItem>();
            Category category = new Category();

            foreach (var row in ranges.Values)
            {
                if (row.Count < 3) { continue; }

                var (HasNew, NewCategory) = TryNewCategory(row);
                if (HasNew) { category = NewCategory; }

                foundFood.Add(
                    ExtractFoodItem(row, category));
            }

            return foundFood;
        }

        /// <summary>
        /// Checks if new category is defined and returns it
        /// </summary>
        /// <param name="row"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static (bool HasNew, Category NewCategory) TryNewCategory(IList<object> row)
        {
            var categoryName = row[0].ToString();
            var hasName = !string.IsNullOrWhiteSpace(categoryName);
            var newCategory = new Category(Constants.CATEGORY_STANDARD, categoryName);

            return (hasName, newCategory);
        }

        /// <summary>
        /// Extract single food item from a row.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static FoodItem ExtractFoodItem(IList<object> row, Category category)
        {
            return new FoodItem(
                    ExtractName(row),       //Name
                    ExtractPrice(row),      //Price
                    Constants.INDEX_NAME,   //Restaurant
                    category,               //Category
                    string.Empty);          //Description
        }

        /// <summary>
        /// Extracts food name from the row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static string ExtractName(IList<object> row)
        {
            return row[1].ToString();
        }

        /// <summary>
        /// Extracts food price from the row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static double ExtractPrice(IList<object> row)
        {
            return double.Parse(row[2].ToString());
        } 
        #endregion
    }
}
