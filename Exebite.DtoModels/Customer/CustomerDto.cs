using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class CustomerDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public LocationDto DefaultLocation { get; set; }

        public string GoogleUserId { get; set; }

        public List<OrderDto> Orders { get; set; }

        public List<MealDto> FavouriteMeals { get; set; }
    }
}
