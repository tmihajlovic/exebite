using System;
using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateOrderDto
    {
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int MealId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Note { get; set; }
    }
}
