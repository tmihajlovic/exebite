namespace Exebite.DomainModel
{
    public class LocationQueryModel
    {
        public int? Id { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}";
        }
    }
}
