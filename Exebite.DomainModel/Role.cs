using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
