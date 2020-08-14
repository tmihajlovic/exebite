﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Exebite.DataAccess.Context
{
    /// <summary>
    /// Used only for migrations
    /// </summary>
    public class MigrationsFactory : IDesignTimeDbContextFactory<MealOrderingContext>
    {
        public MealOrderingContext CreateDbContext(string[] args)
        {
            var dbContextOptions = new DbContextOptionsBuilder<MealOrderingContext>()
                .UseSqlServer("Server=(Local);Database=Exebite;Trusted_Connection=True;")
                .UseLazyLoadingProxies().Options;

            return new MealOrderingContext(dbContextOptions);
        }
    }
}
