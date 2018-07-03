using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateLocationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
