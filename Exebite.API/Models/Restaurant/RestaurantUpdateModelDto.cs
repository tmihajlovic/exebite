using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class RestaurantUpdateModelDto
    {
        [Required]
        public string Name { get; set; }
    }
}
