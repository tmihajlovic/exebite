using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Execom.ClientDemo.Models
{
    public class HomeIndexViewModel
    {
        public List<Food> TodayFoods { get; set; }
        public List<Order> ListOfOrders { get; set; }
        public Order CurentOrder { get; set; }
        public Customer Customer { get; set; }
        public List<Restaurant> ListOfRestaurants { get; set; }
    }

    public class HomeIstorijaNarudzbinaViewModel
    {
        public Customer Customer { get; set; }
    }
}