using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class RestaurantInsertModelDto
    {
        [Required]
        public string Name { get; set; }
    }
}
