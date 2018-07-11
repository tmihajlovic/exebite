namespace Exebite.DomainModel
{
    public class CustomerAliases
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public Restaurant Restaurant { get; set; }

        public string Alias { get; set; }
    }
}
