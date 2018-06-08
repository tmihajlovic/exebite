using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.API.Models
{
    public class OrdersViewModel
    {
        public List<Food> TodayFoods { get; set; } = new List<Food>();
        public List<Order> ListOfOrders { get; set; } = new List<Order>();
        public Order CurentOrder { get; set; } = new Order();
        public Customer Customer { get; set; } = new Customer();
        public List<Restaurant> ListOfRestaurants { get; set; } = new List<Restaurant>();
    }
}