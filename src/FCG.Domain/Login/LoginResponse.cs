using MessagePack;

namespace FCG.Domain.Login;

[MessagePackObject]
public class LoginResponse(int UserName, string Role, string email, string Token)
{
    [Key(0)]
    public int UserName { get; set; } = UserName;

    [Key(1)]
    public string Role { get; set; } = Role;

    [Key(2)]
    public string Email { get; set; } = email;

    [Key(3)]
    public string Token { get; set; } = Token;
}