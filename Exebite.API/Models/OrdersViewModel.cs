using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.API.Models
{
    public class OrdersViewModel
    {
        public List<Food> TodayFoods { get; set; }
        public List<Order> ListOfOrders { get; set; }
        public Order CurentOrder { get; set; }
        public Customer Customer { get; set; }
        public List<Restaurant> ListOfRestaurants { get; set; }
    }
}