using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Model
{
    public class Meal
    {
        public int Id { get; set; }

        public List<Food> Foods { get; set; }

        public decimal Price { get; set; }
    }
}
