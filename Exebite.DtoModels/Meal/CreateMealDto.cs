using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateMealDto
    {
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public List<int> Foods { get; set; }
    }
}
