using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces
{
    public interface IRestaurantConector
    {
        List<Food> GetDailyMenu();
        List<Food> LoadAllFoods();
        void PlaceOrders(List<Order> orders);
        void WriteMenu(List<Food> foods);
        void WriteKasaTab(List<Customer> customerList);
    }
}
