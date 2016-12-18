using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Repositories.Contexts;

namespace WhereWereWe.Repositories.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SeriesContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<SeriesProgressContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISeriesProgressRepository, SeriesProgressRepository>();

            return services;
        }
    }
}
