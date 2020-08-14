using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
{
    public interface IExebiteDbContextOptionsFactory
    {
        DbContextOptions<MealOrderingContext> Create();
    }
}