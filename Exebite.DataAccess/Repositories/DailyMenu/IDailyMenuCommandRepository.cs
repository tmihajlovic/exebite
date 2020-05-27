namespace Exebite.DataAccess.Repositories
{
    public interface IDailyMenuCommandRepository : IDatabaseCommandRepository<long, DailyMenuInsertModel, DailyMenuUpdateModel>
    {
    }
}
