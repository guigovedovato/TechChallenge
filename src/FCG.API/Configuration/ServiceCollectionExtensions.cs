using FCG.Application.Configuration;
using FCG.Infrastructure.Configuration;

namespace FCG.API.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        services.RegisterInfrastructure();
        services.RegisterServices();

        return services;
    }
}