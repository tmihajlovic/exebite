using System;
using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class DailyMenuInsertModel
    {
        public long RestaurantId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public List<Meal> Meals { get; set; }
    }
}