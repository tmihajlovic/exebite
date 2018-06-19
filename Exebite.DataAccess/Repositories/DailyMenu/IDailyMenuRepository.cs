using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IDailyMenuRepository : IDatabaseRepository<DailyMenu, DailyMenuQueryModel>
    {
    }
}
