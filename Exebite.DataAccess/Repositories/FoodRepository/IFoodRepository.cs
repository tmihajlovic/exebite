using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IFoodRepository : IDatabaseRepository<Food, FoodQueryModel>
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
