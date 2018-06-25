using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateDailyMenuModel
    {
        [Required]
        public int RestaurantId { get; set; }
    }
}
