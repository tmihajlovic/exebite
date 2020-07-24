namespace Exebite.DataAccess.Repositories
{
    public class CustomerUpdateModel
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string GoogleUserId { get; set; }

        public int Role { get; set; }

        public long DefaultLocationId { get; set; }

        public bool IsActive { get; set; }
    }
}
