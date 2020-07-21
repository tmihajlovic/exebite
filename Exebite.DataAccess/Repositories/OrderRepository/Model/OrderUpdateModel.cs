using System;
using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class OrderUpdateModel
    {
        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public long LocationId { get; set; }

        public long CustomerId { get; set; }

        public List<Meal> Meals { get; set; }
    }
}