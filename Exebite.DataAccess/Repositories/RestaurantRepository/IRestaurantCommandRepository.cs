namespace Exebite.DataAccess.Repositories
{
    public interface IRestaurantCommandRepository : IDatabaseCommandRepository<int, RestaurantInsertModel, RestaurantUpdateModel>
    {
    }
}
