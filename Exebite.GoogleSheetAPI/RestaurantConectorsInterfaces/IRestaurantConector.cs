using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces
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
