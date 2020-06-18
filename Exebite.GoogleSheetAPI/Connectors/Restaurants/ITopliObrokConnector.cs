using System.Collections.Generic;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public interface ITopliObrokConnector : IRestaurantConnector
    {
        /// <summary>
        /// Order Daily menu sheet so first column is today and place correct dates
        /// </summary>
        void DnevniMenuSheetSetup();

        /// <summary>
        /// Gets meals available for today
        /// </summary>
        /// <returns>List of meals</returns>
        List<Meal> GetDailyMenu();
    }
}