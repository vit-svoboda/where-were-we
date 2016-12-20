using Microsoft.Extensions.DependencyInjection;
using WhereWereWe.Domain.Interfaces;

namespace WhereWereWe.Services.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWhereWereWeServices(this IServiceCollection services)
        {
            services.AddTransient<IProgressTrackingService, ProgressTrackingService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
