using FCG.Domain.Common;
using FCG.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace FCG.Domain.Login;

public record LoginModel : EntityBase
{
    public required string Email { get; set; }
    public required string Password { get; set; }

    public virtual UserModel User { get; set; } = null!;

    public void HashPassword()
    {
        var hasher = new PasswordHasher<string>();
        Password = hasher.HashPassword(Email, Password);
    }

    public bool VerifyPassword(string password)
    {
        var hasher = new PasswordHasher<string>();
        var result = hasher.VerifyHashedPassword(Email, Password, password);
        return result is PasswordVerificationResult.Success;
    }
}