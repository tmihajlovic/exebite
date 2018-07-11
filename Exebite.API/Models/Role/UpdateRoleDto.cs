using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
