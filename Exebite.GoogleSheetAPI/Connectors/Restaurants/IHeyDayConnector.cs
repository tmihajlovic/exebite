using System.Collections.Generic;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public interface IHeyDayConnector : IRestaurantConnector
    {
        /// <summary>
        /// Get all meals from the main menu
        /// </summary>
        /// <returns>List of meals</returns>
        List<Meal> GetMainMenu();
    }
}
