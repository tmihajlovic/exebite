using System;

namespace Exebite.DtoModels
{
    public class OrderDto
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public int MealId { get; set; }

        public int CustomerId { get; set; }

        public string Note { get; set; }
    }
}