using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddDbContext<BilliardDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BilliardConnectionString")));

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.AddScoped<ICueRepositoty,CueRepository>();
            services.AddScoped<IBrandRepository,BrandRepository>();
            services.AddScoped<IOrderItemRepository,OrderItemRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            return services;
        }
    }
}
