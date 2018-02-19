using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Model
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FoodType Type { get; set; }

        public decimal Price { get; set; }

        public Restaurant Restaurant { get; set; }

    }

    public enum FoodType
    {
        MAIN_COURSE,
        DESERT,
        SALAD,
        SIDE_DISH,
        SOUP,
        CONDIMENTS
    }
}
