namespace FCG.Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(string userName, string role);
}