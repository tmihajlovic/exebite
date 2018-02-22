using Exebite.Model;

namespace Exebite.DataAccess
{
    public interface IRestaurantRepository : IDatabaseRepository<Restaurant>
    {
        // Add functions specific for IRestaurantHandler
        Restaurant GetByName(string name);
    }
}
