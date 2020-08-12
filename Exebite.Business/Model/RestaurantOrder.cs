using System;
using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.Business.Model
{
    public class RestaurantOrder
    {
        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public int LocationId { get; set; }

        public int CustomerId { get; set; }

        public List<Meal> Meals { get; set; }
    }
}
