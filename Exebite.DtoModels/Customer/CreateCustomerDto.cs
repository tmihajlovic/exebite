using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        public int? LocationId { get; set; }

        public string GoogleUserId { get; set; }
    }
}
