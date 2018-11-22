using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class RestaurantUpdateModelDto
    {
        [Required]
        public string Name { get; set; }
    }
}
