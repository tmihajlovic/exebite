using System;
using System.Collections.Generic;

namespace ExeBite.Sheets.Common.Interfaces
{
    /// <summary>
    /// Interface that defines what methods must be implemented
    /// for each Restaurant.
    /// </summary>
    public interface IRestaurantReader
    {
        /// <summary>
        /// Reads Food items that are always offered.
        /// </summary>
        /// <returns></returns>
        IEnumerable<FoodItem> ReadFoodItems();

        /// <summary>
        /// Reads daily food items.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<DailyOfferFood> ReadDailyOffers(DateTime date);
    }
}
