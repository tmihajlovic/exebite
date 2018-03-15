using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exebite.Model;

namespace Exebite.DataAccess.Entities
{
    [Table(nameof(Location))]
    public class LocationEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
