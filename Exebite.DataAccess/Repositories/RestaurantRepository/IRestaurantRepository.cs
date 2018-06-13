using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public interface IRestaurantRepository : IDatabaseRepository<Restaurant, RestaurantQueryModel>
    {
        // Add functions specific for IRestaurantHandler

        /// <summary>
        /// Get <see cref="Restaurant"/> by name
        /// </summary>
        /// <param name="name">Name of restaurant</param>
        /// <returns><see cref="Restaurant"/></returns>
        Restaurant GetByName(string name);
    }
}
