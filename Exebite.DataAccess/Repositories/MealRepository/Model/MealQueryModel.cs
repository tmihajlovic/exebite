namespace Exebite.DataAccess.Repositories
{
    public class MealQueryModel : QueryBase
    {
        public MealQueryModel()
        {
        }

        public MealQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }
    }
}
