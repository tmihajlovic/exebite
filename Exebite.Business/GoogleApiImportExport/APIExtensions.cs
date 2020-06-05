using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DomainModel;

namespace Exebite.Business.GoogleApiImportExport
{
    public static class APIExtensions
    {
        //public static(bool Found, List<Meal> StandardOffer, List<Meal> DailyOffer)
        //    GetRestaurantOffersForDate(this List<RestaurantOffer> offers, Restaurant restaurant)
        //{
        //    var restaurantList = offers.Where(item => item.RestaurantName.Equals(restaurant.Name));
        //    var found = restaurantList.Any();
        //    var standard = new List<Meal>();
        //    var daily = new List<Meal>();
        //    if (found)
        //    {
        //        standard = restaurantList.First()
        //            .StandardOffers
        //            .Select(item => item.ToMeal(restaurant))
        //            .ToList();

        //        daily = restaurantList.First()
        //            .DailyOffers
        //            .Select(item => item.ToMeal(restaurant))
        //            .ToList();
        //    }

        //    return (found, standard, daily);
        //}
    }
}
