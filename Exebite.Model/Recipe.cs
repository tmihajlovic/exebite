using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Model
{
    public class Recipe
    {
        public int Id { get; set; }

        public Restaurant Restaurant { get; set; }

        public Food MainCourse { get; set; }

        public List<Food> SideDish { get; set; }
    }
}
