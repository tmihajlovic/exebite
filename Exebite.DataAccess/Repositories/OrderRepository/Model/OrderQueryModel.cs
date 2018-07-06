using System;

namespace Exebite.DataAccess.Repositories
{
    public class OrderQueryModel : QueryBase
    {
        public OrderQueryModel()
        {
        }

        public OrderQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }

        // TODO: change this to be our class with data only
        public DateTime? Date { get; set; }

        public int? CustomerId { get; set; }
    }
}
