using Exebite.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.DataAccess.Context
{
    /// <summary>
    /// Used only for migrations
    /// </summary>
    public class MladenTestDbContextFactory : IDesignTimeDbContextFactory<FoodOrderingContext>
    {
        public FoodOrderingContext CreateDbContext(string[] args)
        {

            var dbContextOptions = new DbContextOptionsBuilder<FoodOrderingContext>()
                .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Exebite;Trusted_Connection=True;")
                .UseLazyLoadingProxies().Options;

            return new FoodOrderingContext(dbContextOptions);
        }
    }
}
