using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class MealDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public bool IsActive { get; set; }

        public long RestaurantId { get; set; }

        public virtual List<MealDto> Condiments { get; set; }
    }
}
