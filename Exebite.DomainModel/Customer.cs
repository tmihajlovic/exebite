using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Customer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string GoogleUserId { get; set; }

        public int Role { get; set; }

        public Location DefaultLocation { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Meal> FavouriteMeals { get; set; } = new List<Meal>();


    }
}
