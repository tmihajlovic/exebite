using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantHandler)
        {
            _restaurantRepository = restaurantHandler;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _restaurantRepository.GetByID(id);
        }

        public Restaurant GetRestaurantByName(string name)
        {
            return _restaurantRepository.GetByName(name);
        }

        public Restaurant CreateNewRestaurant(Restaurant restaurant)
        {
            return _restaurantRepository.Insert(restaurant);
        }

        public Restaurant UpdateRestourant(Restaurant restaurant)
        {
            return _restaurantRepository.Update(restaurant);
        }

        public void DeleteRestourant(int restaurantId)
        {
            _restaurantRepository.Delete(restaurantId);
        }
    }
}
