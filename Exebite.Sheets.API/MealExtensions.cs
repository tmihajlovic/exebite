using Exebite.DomainModel;
using Exebite.Sheets.Common.Models;
using System;

namespace Exebite.Sheets.API
{
    public static class MealExtensions
    {
        /// <summary>
        /// Converts Daily offer meal to Domain Model to be used in database.
        /// </summary>
        /// <param name="daily"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public static DomainModel.Meal ToMeal(this DailyOfferMeal daily, Restaurant restaurant)
        {
            return new DomainModel.Meal()
            {
                Name = daily.Name,
                Price = Convert.ToDecimal(daily.Price),
                IsActive = false,
                Restaurant = restaurant
            };
        }

        /// <summary>
        /// Converts standard offer meal to Domain Model to be used in database.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public static DomainModel.Meal ToFood(this MealItem item, Restaurant restaurant)
        {
            return new DomainModel.Meal()
            {
                Name = item.Name,
                Price = Convert.ToDecimal(item.Price),
                IsActive = true,
                Restaurant = restaurant,
                Description = item.Description
            };
        }
    }
}
