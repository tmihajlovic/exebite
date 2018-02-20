using Exebite.DataAccess.Handlers;
using Exebite.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Handlers
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
