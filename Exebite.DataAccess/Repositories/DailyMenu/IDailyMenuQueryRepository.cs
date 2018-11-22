using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IDailyMenuQueryRepository : IDatabaseQueryRepository<DailyMenu, DailyMenuQueryModel>
    {
    }
}
