using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WhereWereWe.Domain.Interfaces;

namespace WhereWereWe.Repositories.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SeriesContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
