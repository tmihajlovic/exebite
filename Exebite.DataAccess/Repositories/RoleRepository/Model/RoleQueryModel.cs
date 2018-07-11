namespace Exebite.DataAccess.Repositories
{
    public class RoleQueryModel : QueryBase
    {
        public RoleQueryModel()
        {
        }

        public RoleQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }
    }
}
