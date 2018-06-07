using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exebite.API.Models
{
    public class UpdateOrderModel
    {
        public int Id { get; set; }

        public int[] FoodIds { get; set; }
    }
}
