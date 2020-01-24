using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants.Base
{
    public interface IRestaurantConnector
    {
        List<Food> GetDailyMenu();

        List<Food> LoadAllFoods();

        void PlaceOrders(List<Order> orders);

        void WriteMenu(List<Food> foods);

        void WriteKasaTab(List<Customer> customerList);
    }
}
