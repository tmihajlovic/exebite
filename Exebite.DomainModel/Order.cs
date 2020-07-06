using System;
using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Order
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public Customer Customer { get; set; }

        public Location Location { get; set; }

        public string Note { get; set; }

        public List<OrderToMeal> OrdersToMeals { get; set; } = new List<OrderToMeal>();
    }
}
