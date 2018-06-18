using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }


        public virtual List<Order> Orders { get; set; }

        public virtual List<CustomerAliases> Aliases { get; set; }

        public string AppUserId { get; set; }
    }
}
