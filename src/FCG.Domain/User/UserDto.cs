using FCG.Domain.Login;

namespace FCG.Domain.User;

public class UserDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Role Role { get; set; }
    public bool IsActive { get; set; }
    public required string Email { get; set; }
    public string Password { get; set; } = string.Empty;

    public override string ToString() => $"{FirstName} {LastName}";

    public UserModel ToUserModel() => new()
    {
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        FirstName = FirstName,
        LastName = LastName,
        Role = Role,
        Login = new LoginModel
        {
            Email = Email,
            Password = Password
        }
    };

    public UserResponse ToUserResponse() => new(Id, CreatedAt, UpdatedAt, FirstName, LastName, Role, Email, IsActive);
}