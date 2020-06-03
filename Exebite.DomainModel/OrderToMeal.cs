using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.DomainModel
{
    public class OrderToMeal
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }

        public long? OrderId { get; set; }

        public Order Order { get; set; }

        public long? MealId { get; set; }

        public Meal Meal { get; set; }
    }
}
