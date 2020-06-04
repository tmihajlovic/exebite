using System;

namespace Exebite.DtoModels
{
    public class UpdatePaymentDto
    {
        public DateTime Date { get; set; }

        public long CustomerId { get; set; }

        public decimal Amount { get; set; }
    }
}