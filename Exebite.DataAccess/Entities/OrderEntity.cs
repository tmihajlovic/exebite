using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Order")]
    public class OrderEntity
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }

        public virtual CustomerEntity Customer { get; set; }

        [ForeignKey(nameof(Location))]
        public long LocationId { get; set; }

        public virtual LocationEntity Location { get; set; }

        public virtual List<OrderToMealEntity> OrdersToMeals { get; set; } = new List<OrderToMealEntity>();
    }
}
