using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [Required]
        public long DefaultLocationId { get; set; }

        [Required]
        public string GoogleUserId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int Role { get; set; }
    }
}
