using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exebite.DataAccess.Entities
{
    [Table("Payment")]
    public class PaymentEntity
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }

        public virtual CustomerEntity Customer { get; set; }
    }
}
