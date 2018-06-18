using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IMealRepository : IDatabaseRepository<Meal, MealQueryModel>
    {
    }
}