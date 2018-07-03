using System.ComponentModel.DataAnnotations;

namespace Exebite.API.Models
{
    public class UpdateDailyMenuDto
    {
        [Required]
        public int RestaurantId { get; set; }
    }
}
