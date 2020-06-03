using System;

namespace Exebite.DomainModel
{
    public class Payment
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public decimal Amount { get; set; }
    }
}
