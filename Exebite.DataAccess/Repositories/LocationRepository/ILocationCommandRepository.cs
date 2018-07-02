namespace Exebite.DataAccess.Repositories
{
    public interface ILocationCommandRepository : IDatabaseCommandRepository<int, LocationInsertModel, LocationUpdateModel>
    {
    }
}
