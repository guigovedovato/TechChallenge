using FCG.Domain.Login;

namespace FCG.API.Endpoints;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        var loginGroup = app
            .MapGroup("/login")
            .WithOpenApi();

        loginGroup.MapPost("/", Login);
        loginGroup.MapPost("/logout", Logout).RequireAuthorization();
    }

    static async Task<IResult> Login(LoginRequest loginRequest, ILoginService loginService)
    {
        var loggedUser = await loginService.LoginAsync(loginRequest.ToLoginDto(), new CancellationToken());
        return string.IsNullOrWhiteSpace(loggedUser?.Token) ? 
            TypedResults.Unauthorized() : TypedResults.Ok(loggedUser.Token);
    }

    static async Task<IResult> Logout(LoginRequest loginRequest, ILoginService loginService)
    {
        var loggedUser = await loginService.LogoutAsync(loginRequest.ToLoginDto(), new CancellationToken());
        return string.IsNullOrWhiteSpace(loggedUser?.Token) ?
            TypedResults.Ok() : TypedResults.Problem("Logout failed");
    }
}
