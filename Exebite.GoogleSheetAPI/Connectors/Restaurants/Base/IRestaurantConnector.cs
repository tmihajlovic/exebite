using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants.Base
{
    public interface IRestaurantConnector
    {
        void WriteMenu(List<Meal> foods);
    }
}
