using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exebite.DtoModels;

    public class TempContext : DbContext
    {
        public TempContext (DbContextOptions<TempContext> options)
            : base(options)
        {
        }

        public DbSet<Exebite.DtoModels.LocationDto> LocationDto { get; set; }

        public DbSet<Exebite.DtoModels.OrderDto> OrderDto { get; set; }
    }
