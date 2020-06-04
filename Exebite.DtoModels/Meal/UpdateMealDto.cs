using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Exebite.DtoModels
{
    public class UpdateMealDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public List<long> Condiments { get; set; }
    }
}
