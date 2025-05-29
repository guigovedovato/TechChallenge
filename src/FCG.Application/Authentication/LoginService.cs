using FCG.Domain.Authentication;
using FCG.Domain.Profile;
using FCG.Infrastructure.Authentication;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;

namespace FCG.Application.Authentication;

public class LoginService(IJwtTokenGenerator _jwtGenerator, ILoginRepository _loginRepository, BaseLogger _logger) : ILoginService
{
    public async Task<LoginDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Logging in user: {loginDto.Email}");

        var loginModel = await _loginRepository.GetByEmailAsync(loginDto.Email, cancellationToken);

        if (loginModel is null)
        {
            _logger.LogWarning($"User not found: {loginDto.Email}");
            return default!;
        }

        if (!loginModel.VerifyPassword(loginDto.Password))
        {
            _logger.LogWarning($"Invalid password for user: {loginDto.Email}");
            return default!;
        }

        _logger.LogInformation($"User logged in: {loginDto.Email}");

        var loggedUer = new LoginDto
        {
            Email = loginDto.Email,
            Token = await _jwtGenerator.GenerateTokenAsync(loginDto.Email, loginModel.User.Role.ToString())
        };
        return loggedUer;
    }

    public Task<LoginDto> LogoutAsync(LoginDto loginDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
