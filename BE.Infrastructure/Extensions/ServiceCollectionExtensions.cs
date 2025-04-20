using BE.Domain.Repositories;
using BE.Infrastructure.Persistence;
using BE.Infrastructure.Repositories;
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
            services.AddDbContext<ImmobilienDbContext>(
                options =>
                {
                    options
                    .UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging();
                });

            services.AddScoped<IImmobilienOverviewRepository, ImmobilienOverviewRepository>();
            services.AddScoped<IImmobilienTypeRepository, ImmobilienTypeRepository>();
            services.AddScoped<IImmobilienHausgeldRepository, ImmobilienHausgeldRepository>();

        }
    }
}
