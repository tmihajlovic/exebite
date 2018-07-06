namespace Exebite.DataAccess.Repositories
{
    public class CustomerAliasQueryModel : QueryBase
    {
        public CustomerAliasQueryModel()
        {
        }

        public CustomerAliasQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }
    }
}
