using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class UpdateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
