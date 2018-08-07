using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class UpdateLocationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
