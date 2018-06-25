using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class CreateDailyMenuModel
    {
        [Required]
        public int RestaurantId { get; set; }
    }
}
