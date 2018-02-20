using Exebite.Model;

namespace Exebite.DataAccess
{
    public interface IRestaurantHandler : IDatabaseHandler<Restaurant>
    {
        // Add functions specific for IRestaurantHandler
        Restaurant GetByName(string name);
    }
}
