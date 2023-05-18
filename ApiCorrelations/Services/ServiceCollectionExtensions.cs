using ApiCorrelations.Configurations;
using ApiCorrelations.Configurations.Interfaces;

namespace ApiCorrelations.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdManager(this IServiceCollection services)
        {
            services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();

            return services;
        }
    }
}
