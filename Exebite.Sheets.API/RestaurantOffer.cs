
using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.Sheets.API
{
    public class RestaurantOffer
    {
        /// <summary>
        /// Date of the offers included
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Name of the restaurant
        /// </summary>
        public string RestaurantName { get; private set; }

        /// <summary>
        /// Food offered for this day specificaly
        /// </summary>
        public List<DailyOfferFood> DailyOffers { get; private set; }

        /// <summary>
        /// Food offered any day
        /// </summary>
        public List<FoodItem> StandardOffers { get; private set; }

        /// <summary>
        /// Constructor providing 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="restaurantName"></param>
        /// <param name="dailyOffers"></param>
        /// <param name="standardOffers"></param>
        public RestaurantOffer(
            DateTime date,
            string restaurantName,
            List<DailyOfferFood> dailyOffers,
            List<FoodItem> standardOffers)
        {
            Date = date;
            RestaurantName = restaurantName;
            DailyOffers = dailyOffers;
            StandardOffers = standardOffers;
        }
    }
}
