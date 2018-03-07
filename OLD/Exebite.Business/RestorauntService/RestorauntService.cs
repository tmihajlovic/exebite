using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class RestarauntService : IRestarauntService
    {
        IRestaurantRepository _restaurantRepository;

        public RestarauntService(IRestaurantRepository restaurantHandler)
        {
            _restaurantRepository = restaurantHandler;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }

        public Restaurant GetRestaurantById(int Id)
        {
            return _restaurantRepository.GetByID(Id);
        }

        public Restaurant GetRestaurantByName(string name)
        {
            return _restaurantRepository.GetByName(name);
        }

        public void CreateNewRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.Insert(restaurant);
        }

        public void UpdateRestourant(Restaurant restaurant)
        {
            _restaurantRepository.Update(restaurant);
        }

        public void DeleteRestourant(int restaurantId)
        {
            _restaurantRepository.Delete(restaurantId);
        }
    }
}
