using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateCustomerDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public string AppUserId { get; set; }
    }
}
