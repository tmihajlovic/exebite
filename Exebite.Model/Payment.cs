using System;

namespace Exebite.Model
{
    public class Payment
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
