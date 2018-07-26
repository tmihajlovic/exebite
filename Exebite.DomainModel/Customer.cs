using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string GoogleId { get; set; }

        public int? LocationId { get; set; }

        public Location Location { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public List<Order> Orders { get; set; }

        public List<CustomerAliases> Aliases { get; set; }


    }
}
