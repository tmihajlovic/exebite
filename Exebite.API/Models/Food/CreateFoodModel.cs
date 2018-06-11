using Exebite.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exebite.API.Models
{
    public class CreateFoodModel
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FoodType Type { get; set; }

        public decimal Price { get; set; }

        public int RestaurantId { get; set; }

        public string Description { get; set; }

        public bool IsInactive { get; set; }
    }
}
