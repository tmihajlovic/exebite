using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Customer")]
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string GoogleUserId { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }

        public virtual LocationEntity Location { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual RoleEntity Role { get; set; }

        public virtual List<OrderEntity> Orders { get; set; }

        public virtual List<CustomerAliasesEntities> Aliases { get; set; }
    }
}
