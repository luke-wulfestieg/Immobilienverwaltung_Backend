using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using BE.Infrastructure.Repositories;
using BE.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BE.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RestaurantsDbContext>(
                options =>
                {
                    options
                    .UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging();
                });

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
        }
    }
}
