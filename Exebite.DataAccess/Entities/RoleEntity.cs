using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Role")]
    public class RoleEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<CustomerEntity> Customers { get; set; }
    }
}
