using FCG.Application.Authentication;
using FCG.Application.Profile;
using FCG.Application.Store;
using FCG.Domain.Authentication;
using FCG.Domain.Profile;
using FCG.Domain.Store;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}