using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestRestaurant.Models
{
    public class RestauranteContextFactory : IDesignTimeDbContextFactory<RestauranteContext>
    {
        public RestauranteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestauranteContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Restaurante;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new RestauranteContext(optionsBuilder.Options);
        }
    }
}
