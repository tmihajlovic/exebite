using ExeBite.Sheets.Common;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExeBite.Sheets.Teglas
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
            var subcategory = string.Empty;

            foreach (var row in ranges.Values)
            {
                if (row.Count < 3)
                {
                    if (row.Count > 0)
                    {
                        subcategory = row[0].ToString();
                    }
                    continue;
                }

                foundFood.Add(new FoodItem(
                    row[0].ToString(),                  //Name
                    double.Parse(row[2].ToString()),    //Price
                    Constants.TEGLAS_NAME,              //Restaurant
                    subcategory,                        //Subcategory
                    row[1].ToString()));                //Description
            }

            return foundFood;
        }
    }
}
