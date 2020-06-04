using System;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateRestaurantDto
    {
        [Required]
        public string SheetId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string LogoUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime? OrderDue { get; set; }
    }
}
