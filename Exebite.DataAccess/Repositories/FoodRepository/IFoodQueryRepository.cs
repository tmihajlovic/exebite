using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IFoodQueryRepository : IDatabaseQueryRepository<Food, FoodQueryModel>
    {
    }
}
