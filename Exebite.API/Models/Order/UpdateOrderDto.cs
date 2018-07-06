using System;
using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateOrderDto
    {
        [Required]
        public int[] FoodIds { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int MealId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}