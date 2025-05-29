namespace FCG.Domain.Authentication;

public interface ILoginService
{
    Task<LoginDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
    Task<LoginDto> LogoutAsync(LoginDto loginDto, CancellationToken cancellationToken);
}
