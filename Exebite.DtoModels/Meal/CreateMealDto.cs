using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateMealDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsFromStandardMenu { get; set; }
    }
}
