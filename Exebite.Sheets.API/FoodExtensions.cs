using Exebite.DomainModel;
using Exebite.Sheets.Common.Models;
using System;

namespace Exebite.Sheets.API
{
    public static class FoodExtensions
    {
        /// <summary>
        /// Converts Daily offer food to Domain Model food to be used in database.
        /// </summary>
        /// <param name="daily"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public static DomainModel.Food ToFood(this DailyOfferFood daily, Restaurant restaurant)
        {
            return new DomainModel.Food()
            {
                Name = daily.Name,
                Price = Convert.ToDecimal(daily.Price),
                IsInactive = false,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id
            };
        }

        /// <summary>
        /// Converts standard offer food to Domain Model food to be used in database.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public static DomainModel.Food ToFood(this FoodItem item, Restaurant restaurant)
        {
            return new DomainModel.Food()
            {
                Name = item.Name,
                Price = Convert.ToDecimal(item.Price),
                IsInactive = false,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id,
                Description = item.Description
            };
        }
    }
}
