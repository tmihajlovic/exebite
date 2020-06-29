using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class Meal
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public bool IsActive { get; set; }

        public bool IsFromStandardMenu { get; set; }

        public Restaurant Restaurant { get; set; }

        public List<Meal> Condiments { get; set; } = new List<Meal>();
    }
}
