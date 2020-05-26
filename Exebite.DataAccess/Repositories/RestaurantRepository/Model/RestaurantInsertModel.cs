using System;

namespace Exebite.DataAccess.Repositories
{
    public class RestaurantInsertModel
    {
        public string SheetId { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public string Contact { get; set; }

        public DateTime? OrderDue { get; set; }
    }
}