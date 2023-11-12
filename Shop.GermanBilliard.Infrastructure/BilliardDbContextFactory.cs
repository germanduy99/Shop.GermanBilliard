using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Shop.GermanBilliard.Infrastructure
{
   

    public class BilliardDbContextFactory : IDesignTimeDbContextFactory<BilliardDbContext>
    {
        public BilliardDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Shop.GermanBilliard.API"))
                 .AddJsonFile("appsettings.json")
                   .Build();
                
            var optionsBuilder = new DbContextOptionsBuilder<BilliardDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BilliardConnectionString"));
            return new BilliardDbContext(optionsBuilder.Options);
        }
    }
}
