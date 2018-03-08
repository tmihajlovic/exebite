using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(CustomerAliasesEntities))]
    public class CustomerAliasesEntities
    {
        [Key]
        public int Id { get; set; }

        public string Alias { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual CustomerEntity Customer { get; set; }

        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual RestaurantEntity Restaurant { get; set; }

    }
}
