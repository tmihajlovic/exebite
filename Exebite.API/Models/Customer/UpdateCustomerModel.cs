using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateCustomerModel
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public int LocationId { get; set; }

        public string AppUserId { get; set; }
    }
}
