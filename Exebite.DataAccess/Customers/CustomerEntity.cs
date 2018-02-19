using Exebite.DataAccess.Locations;
using Exebite.DataAccess.Orders;
using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Customers
{
    [Table(nameof(Customer))]
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual LocationEntity Location { get; set; }

        public virtual List<OrderEntity> Orders{ get; set; }
    }
}
