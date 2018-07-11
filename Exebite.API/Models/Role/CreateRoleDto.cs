using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
