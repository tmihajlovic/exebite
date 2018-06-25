using System.ComponentModel.DataAnnotations;
using Exebite.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exebite.API.Models
{
    public class UpdateFoodModel
    {
        [Required]
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public FoodType Type { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsInactive { get; set; }
    }
}
