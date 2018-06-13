using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateCustomerModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public int LocationId { get; set; }

        public string AppUserId { get; set; }
    }
}
