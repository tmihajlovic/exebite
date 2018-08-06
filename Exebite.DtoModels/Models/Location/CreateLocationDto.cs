using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels.Models
{
    public class CreateLocationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
