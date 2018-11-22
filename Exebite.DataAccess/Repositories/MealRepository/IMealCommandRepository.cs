namespace Exebite.DataAccess.Repositories
{
    public interface IMealCommandRepository : IDatabaseCommandRepository<int, MealInsertModel, MealUpdateModel>
    {
    }
}
