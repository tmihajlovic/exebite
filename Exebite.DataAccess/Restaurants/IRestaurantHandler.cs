using Exebite.Model;

namespace Exebite.DataAccess.Restaurants
{
    public interface IRestaurantHandler : IDatabaseHandler<Restaurant>
    {
        // Add functions specific for IRestaurantHandler
        Restaurant GetByName(string name);
    }
}
