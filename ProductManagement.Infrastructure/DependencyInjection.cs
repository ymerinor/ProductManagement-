using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Repository.Interface;
using ProductManagement.Infrastructure.Repository;

namespace ProductManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductManagementDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("ProductManagementConnection")));

            services.AddTransient<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
