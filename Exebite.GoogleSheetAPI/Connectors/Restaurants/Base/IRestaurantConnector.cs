using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants.Base
{
    public interface IRestaurantConnector
    {
        // List<Meal> GetDailyMenu();

        List<Meal> LoadAllFoods();

        void PlaceOrders(List<Order> orders);

        void WriteMenu(List<Meal> foods);

        void WriteKasaTab(List<Customer> customerList);
    }
}
