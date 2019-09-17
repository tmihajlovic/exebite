namespace Exebite.DataAccess.Repositories
{
    public class LocationQueryModel : QueryBase
    {
        public LocationQueryModel()
        {
        }

        public LocationQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }

        public string Name { get; set; }
    }
}
