using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Business
{
    public interface IRestarauntService
    {
        List<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurantById(int Id);
        Restaurant GetRestaurantByName(string name);
        void CreateNewRestaurant(Restaurant restaurant);
        void UpdateRestourant(Restaurant restaurant);
        void DeleteRestourant(int restaurantId);
    }
}
