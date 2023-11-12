using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Identity
{
    
    public class BilliradIdentityDbContextFactory : IDesignTimeDbContextFactory<BilliradIdentityDbContext>
    {
        public BilliradIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Shop.GermanBilliard.API"))
                 .AddJsonFile("appsettings.json")
                   .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BilliradIdentityDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BilliardIdentityConnectionString"));
            return new BilliradIdentityDbContext(optionsBuilder.Options);
        }
    }
}
