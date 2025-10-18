using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Factories
{
    public static class DatabaseFactoryExtensions
    {
        public static IServiceCollection AddConfiguredDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration["Configurations:UseDatabase"];
            services.CreateDataBase(dbType, configuration);
            return services;
        }
    }
}