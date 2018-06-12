using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.Business
{
    public interface IRestaurantService
    {
        /// <summary>
        /// Gets all restaurants in database
        /// </summary>
        /// <returns>List of all restaurants</returns>
        IList<Restaurant> GetAllRestaurants();

        /// <summary>
        /// Get <see cref="Restaurant"/> by id
        /// </summary>
        /// <param name="id">Id of restaurant</param>
        /// <returns>Restaurant with given Id</returns>
        Restaurant GetRestaurantById(int id);

        /// <summary>
        /// Get <see cref="Restaurant"/> by name
        /// </summary>
        /// <param name="name">Name of restaurant</param>
        /// <returns>Restaurant with given name</returns>
        Restaurant GetRestaurantByName(string name);

        /// <summary>
        /// Create new restaurant
        /// </summary>
        /// <param name="restaurant">New <see cref="Restaurant"/></param>
        /// <returns>New restaurant from database</returns>
        Restaurant CreateNewRestaurant(Restaurant restaurant);

        /// <summary>
        /// Update restaurant info
        /// </summary>
        /// <param name="restaurant">Restaurant with new info</param>
        /// <returns>Updated restaurant from database</returns>
        Restaurant UpdateRestaurant(Restaurant restaurant);

        /// <summary>
        /// Delete restaurant from database
        /// </summary>
        /// <param name="restaurantId">Id of restaurant</param>
        void DeleteRestaurant(int restaurantId);
    }
}
