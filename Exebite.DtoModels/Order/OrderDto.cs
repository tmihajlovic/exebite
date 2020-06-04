using System;
using System.Collections.Generic;

namespace Exebite.DtoModels
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public CustomerDto Customer { get; set; }

        public LocationDto Location { get; set; }

        public List<OrderToMealDto> Meals { get; set; }
    }
}