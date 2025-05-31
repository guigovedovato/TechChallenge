using FCG.Domain.User;

namespace FCG.Domain.Login;

public class LoginDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Token { get; set; } = null!;

    public UserDto User { get; set; } = null!;
}