using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces
{
    public interface IRestaurantConector
    {
        List<Food> GetDalyMenu();
        void PlaceOrders(List<Order> orders);
        void WriteMenu(List<Food> foods);
    }
}
