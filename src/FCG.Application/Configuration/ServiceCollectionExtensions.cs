using FCG.Application.Authentication;
using FCG.Application.User;
using FCG.Application.Game;
using FCG.Domain.Authentication;
using FCG.Domain.User;
using FCG.Domain.Game;
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