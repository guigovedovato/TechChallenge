namespace FCG.Domain.Login;

public class LoginRequest(string Email, string Password)
{
    public string Email { get; set; } = Email;

    public string Password { get; set; } = Password;

    public LoginDto ToLoginDto() => new()
    {
        Email = Email,
        Password = Password
    };
}