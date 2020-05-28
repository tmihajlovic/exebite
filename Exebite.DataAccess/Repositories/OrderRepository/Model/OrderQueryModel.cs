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

        public long? Id { get; set; }

        public DateTime? Date { get; set; }

        public long? CustomerId { get; set; }
    }
}
