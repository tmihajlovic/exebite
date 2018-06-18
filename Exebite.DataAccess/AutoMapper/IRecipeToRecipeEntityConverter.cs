using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.AutoMapper
{
    public interface IRecipeToRecipeEntityConverter : ITypeConverter<Recipe, RecipeEntity>
    {
    }
}
