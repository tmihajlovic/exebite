using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateOrderDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<CreateOrderToMealDto> Meals { get; set; }
    }
}
