using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class CreateDailyMenuDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public List<long> Meals { get; set; }
    }
}
