using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Location")]
    public class LocationEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
