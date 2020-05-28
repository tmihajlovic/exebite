namespace Exebite.DataAccess.Repositories
{
    public interface IRestaurantCommandRepository : IDatabaseCommandRepository<long, RestaurantInsertModel, RestaurantUpdateModel>
    {
    }
}
