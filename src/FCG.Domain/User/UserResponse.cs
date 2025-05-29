using MessagePack;

namespace FCG.Domain.User;

[MessagePackObject]
public class UserResponse(Guid Id, DateTime CreatedAt, DateTime UpdatedAt, string FirstName, string LastName, Role Role, string Email, bool IsActive)
{
    [Key(0)]
    public Guid Id { get; set; } = Id;

    [Key(1)]
    public DateTime CreatedAt { get; set; } = CreatedAt;

    [Key(2)]
    public DateTime UpdatedAt { get; set; } = UpdatedAt;

    [Key(3)]
    public string FirstName { get; set; } = FirstName;

    [Key(4)]
    public string LastName { get; set; } = LastName;

    [Key(5)]
    public Role Role { get; set; } = Role;

    [Key(6)]
    public string Email { get; set; } = Email;

    [Key(7)]
    public bool IsActive { get; set; } = IsActive;
}