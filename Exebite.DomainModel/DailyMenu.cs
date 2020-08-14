using System;
using System.Collections.Generic;

namespace Exebite.DomainModel
{
    public class DailyMenu
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public Restaurant Restaurant { get; set; }

        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
