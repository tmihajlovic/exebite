namespace Exebite.DomainModel
{
    public class CustomerAliasQueryModel
    {
        public int? Id { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}";
        }
    }
}
