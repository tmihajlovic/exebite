using Exebite.DataAccess.Customers;
using Exebite.DataAccess.Meals;
using Exebite.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exebite.DataAccess.Orders
{
    [Table(nameof(Order))]
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }
        public virtual MealEntity Meal { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}
