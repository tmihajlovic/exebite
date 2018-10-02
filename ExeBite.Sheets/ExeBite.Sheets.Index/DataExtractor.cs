using ExeBite.Sheets.Common;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExeBite.Sheets.Index
{
    public class DataExtractor
    {
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
                if (row.Count < 3)
                {
                    continue;
                }

                var newCategory = row[0].ToString();
                if (!string.IsNullOrWhiteSpace(newCategory))
                {
                    category = new Category(Constants.CATEGORY_STANDARD, newCategory);
                }

                foundFood.Add(new FoodItem(
                    row[1].ToString(),                  //Name
                    double.Parse(row[2].ToString()),    //Price
                    Constants.INDEX_NAME,               //Restaurant
                    category,                           //Category
                    string.Empty));                     //Description
            }

            return foundFood;
        }
    }
}
