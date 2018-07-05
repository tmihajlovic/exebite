namespace Exebite.DataAccess.Repositories
{
    public interface IRecipeCommandRepository : IDatabaseCommandRepository<int, RecipeInsertModel, RecipeUpdateModel>
    {
    }
}
