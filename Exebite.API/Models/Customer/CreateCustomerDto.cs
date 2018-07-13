using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public string GoogleUserId { get; set; }

        // TODO: Should this be in insert model? Maybe we should set default User role,
        // and only admin will be able to do role update.
        [Required]
        public int RoleId { get; set; }
    }
}
