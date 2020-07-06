using System;

namespace Exebite.DtoModels
{
    public class PaymentDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public CustomerDto Customer { get; set; }

        public decimal Amount { get; set; }
    }
}