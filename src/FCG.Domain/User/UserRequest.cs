namespace FCG.Domain.User;

public class UserRequest(Guid id, string FirstName, string LastName, Role Role, string Email, string Password)
{
    public Guid Id { get; set; } = id;

    public string FirstName { get; set; } = FirstName;

    public string LastName { get; set; } = LastName;

    public Role Role { get; set; } = Role;

    public string Email { get; set; } = Email;

    public string Password { get; set; } = Password;

    public UserDto ToUserDto() => new()
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
        Role = Role,
        Email = Email,
        Password = Password
    };
}