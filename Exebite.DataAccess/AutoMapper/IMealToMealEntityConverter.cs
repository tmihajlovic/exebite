using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.AutoMapper
{
    public interface IMealToMealEntityConverter : ITypeConverter<Meal, MealEntity>
    {
    }
}