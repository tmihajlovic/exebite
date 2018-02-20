using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class RestarauntService : IRestarauntService
    {
        IRestaurantHandler _restaurantHandler;

        public RestarauntService(IRestaurantHandler restaurantHandler)
        {
            _restaurantHandler = restaurantHandler;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantHandler.Get().ToList();
        }

        public Restaurant GetRestaurantById(int Id)
        {
            return _restaurantHandler.GetByID(Id);
        }

        public Restaurant GetRestaurantByName(string name)
        {
            return _restaurantHandler.GetByName(name);
        }

        public void CreateNewRestaurant(Restaurant restaurant)
        {
            _restaurantHandler.Insert(restaurant);
        }

        public void UpdateRestourant(Restaurant restaurant)
        {
            _restaurantHandler.Update(restaurant);
        }

        public void DeleteRestourant(int restaurantId)
        {
            _restaurantHandler.Delete(restaurantId);
        }
    }
}
