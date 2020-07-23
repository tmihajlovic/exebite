using System.Collections.Generic;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public interface ITopliObrokConnector : IRestaurantConnector
    {
        /// <summary>
        /// Gets meals available for today
        /// </summary>
        /// <returns>List of meals</returns>
        List<Meal> GetDailyMenu();

        void WriteOrder(string customerName, string locationName, List<Meal> meals);
    }
}