using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Model
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Food> DailyMenu { get; set; }

        public List<Food> AlwaysAvailable { get; set; }
        
    }
}
