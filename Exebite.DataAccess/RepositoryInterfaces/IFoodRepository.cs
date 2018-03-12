using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IFoodRepository : IDatabaseRepository<Food>
    {
        // Add functions specific for IOrderHandler

        /// <summary>
        /// Get <see cref="food"/> for given restaurant
        /// </summary>
        /// <param name="restaurant">Restaurant</param>
        /// <returns>List of all restaurant food</returns>
        IEnumerable<Food> GetByRestaurant(Restaurant restaurant);
    }
}
