using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Context
{
    public interface IExebiteDbContextOptionsFactory
    {
        DbContextOptions<FoodOrderingContext> Create();
    }
}