using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateLocationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
