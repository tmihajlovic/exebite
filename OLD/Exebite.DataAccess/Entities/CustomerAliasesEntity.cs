using Exebite.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.DataAccess.Entities
{
    public class CustomerAliasesEntity
    {
        [Key]
        public int Id { get; set; }
        
        public virtual CustomerEntity Customer { get; set; }
        
        public virtual RestaurantEntity Restaurant { get; set; }

        public string Alias { get; set; }
    }
}
