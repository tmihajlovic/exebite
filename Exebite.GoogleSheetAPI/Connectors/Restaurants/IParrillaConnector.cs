using System.Collections.Generic;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public interface IParrillaConnector : IRestaurantConnector
    {
        /// <summary>
        /// Order Daily menu sheet so first column is today and place correct dates
        /// </summary>
        void DnevniMenuSheetSetup();

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
    }
}
