using System;

namespace Exebite.DataAccess.Repositories
{
    public class OrderInsertModel
    {
        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public int MealId { get; set; }

        public int CustomerId { get; set; }
    }
}