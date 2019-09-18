using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public interface IHedoneConector : IRestaurantConector
    {
        /// <summary>
        /// Order Daily menu sheet so first column is today and place corect dates
        /// </summary>
        void DnevniMenuSheetSetup();
    }
}