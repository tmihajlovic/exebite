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

        public long? Id { get; set; }

        public string Name { get; set; }
    }
}
