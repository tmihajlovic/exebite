using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class UpdateMealDto
    {
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public List<int> Foods { get; set; }
    }
}
