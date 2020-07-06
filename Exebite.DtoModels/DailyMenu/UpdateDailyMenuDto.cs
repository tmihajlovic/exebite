using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels
{
    public class UpdateDailyMenuDto
    {
        [Required]
        public long RestaurantId { get; set; }

        [Required]
        public List<long> Meals { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string Note { get; set; }
    }
}
