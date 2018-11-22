using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class RestaurantInsertModelDto
    {
        [Required]
        public string Name { get; set; }
    }
}
