using Exebite.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.DataAccess.Test.InMemoryDB
{
    public class InMemoryDBFactory : IFoodOrderingContextFactory
    {
        private DbContextOptions<FoodOrderingContext> options = new DbContextOptionsBuilder<FoodOrderingContext>().UseInMemoryDatabase("TestDB").UseLazyLoadingProxies(true).Options;

        public FoodOrderingContext Create()
        {
            var db = new FoodOrderingContext(options);
            return db;
        }
    }
}
