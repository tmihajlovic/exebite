namespace Exebite.DataAccess.Repositories
{
    public interface ILocationCommandRepository : IDatabaseCommandRepository<long, LocationInsertModel, LocationUpdateModel>
    {
    }
}
