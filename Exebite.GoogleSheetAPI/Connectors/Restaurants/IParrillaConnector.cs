using System.Collections.Generic;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public interface IParrillaConnector : IRestaurantConnector
    {
        /// <summary>
        /// Get all meals from the main menu
        /// </summary>
        /// <returns>List of meals</returns>
        List<Meal> GetMainMenu();

        /// <summary>
        /// Gets meals available for today
        /// </summary>
        /// <returns>List of meals</returns>
        List<Meal> GetDailyMenu();

        void WriteOrder(string customerName, string locationName, ICollection<Meal> meals);
    }
}
