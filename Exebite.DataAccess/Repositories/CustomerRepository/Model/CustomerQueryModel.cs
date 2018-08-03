namespace Exebite.DataAccess.Repositories
{
    public class CustomerQueryModel : QueryBase
    {
        public CustomerQueryModel()
        {
        }

        public CustomerQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }

        public string GoogleUserId { get; set; }
    }
}
