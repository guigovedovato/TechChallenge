using FCG.Application.Login;
using FCG.Application.User;
using FCG.Application.Game;
using FCG.Domain.Login;
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