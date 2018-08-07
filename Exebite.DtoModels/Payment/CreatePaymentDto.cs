namespace Exebite.DtoModels
{
    public class CreatePaymentDto
    {
        public int CustomerId { get; set; }

        public decimal Amount { get; set; }
    }
}