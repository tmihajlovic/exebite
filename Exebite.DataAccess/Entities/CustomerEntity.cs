using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Customer))]
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string AppUserId { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual LocationEntity Location { get; set; }

        public virtual List<OrderEntity> Orders{ get; set; }

        public virtual List<CustomerAliasesEntity> Aliases { get; set; }
    }
}
