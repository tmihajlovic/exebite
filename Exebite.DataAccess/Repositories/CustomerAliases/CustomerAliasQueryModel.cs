namespace Exebite.DataAccess.Repositories
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
