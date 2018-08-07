using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateRoleDto
    {
        [Required]
        public string Name { get; set; }
    }
}
