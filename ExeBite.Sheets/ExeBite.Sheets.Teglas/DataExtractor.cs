using Exebite.Sheets.Common;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.Sheets.Teglas
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
            var category = new Category();

            foreach (var row in ranges.Values)
            {
                if (row.Count < 3)
                {
                    if (row.Count > 0)
                    {
                        category = new Category(
                            Constants.CATEGORY_STANDARD,
                            row[0].ToString());
                    }
                    continue;
                }

                foundFood.Add(new FoodItem(
                    row[0].ToString(),                  //Name
                    double.Parse(row[2].ToString()),    //Price
                    Constants.TEGLAS_NAME,              //Restaurant
                    category,                           //Subcategory
                    row[1].ToString()));                //Description
            }

            return foundFood;
        }
    }
}
