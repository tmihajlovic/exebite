using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.GoogleSpreadsheetApi
{
    public interface IRestaurantStrategy
    {
        List<Food> GetDailyMenu();
        List<Order> GetHistoricalData();
        void PlaceOrders(List<Order> order);
    }
}
