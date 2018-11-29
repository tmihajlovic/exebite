using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;
using Exebite.Sheets.API;

namespace Exebite.Business.GoogleApiImportExport
{
    public static class APIExtensions
    {
        public static(bool Found, List<Food> StandardOffer, List<Food> DailyOffer)
            GetRestaurantOffersForDate(this List<RestaurantOffer> offers, Restaurant restaurant, DateTime date)
        {
            var restaurantList = offers.Where(item => item.RestaurantName.Equals(restaurant.Name));
            var found = restaurantList.Any();
            var standard = new List<Food>();
            var daily = new List<Food>();
            if (found)
            {
                standard = restaurantList.First()
                    .StandardOffers
                    .Select(item => item.ToFood(restaurant))
                    .ToList();

                daily = restaurantList.First()
                    .DailyOffers
                    .Select(item => item.ToFood(restaurant))
                    .ToList();
            }

            return (found, standard, daily);
        }
    }
}
