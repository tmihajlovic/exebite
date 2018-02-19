using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Model
{
    public class Order
    {
        public int Id { get; set; }

        public decimal Price { get; set; }
        
        public DateTime Date { get; set; }

        public Meal Meal { get; set; }

        public Customer Customer { get; set; }

        public string Note { get; set; }

    }
}
