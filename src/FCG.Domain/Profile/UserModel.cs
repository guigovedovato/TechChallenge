using FCG.Domain.Authentication;
using FCG.Domain.Common;
using System.Text.RegularExpressions;

namespace FCG.Domain.Profile;

public partial record UserModel : EntityBase
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Role Role { get; set; }
    public bool IsActive { get; set; } = true;

    public Guid LoginId { get; set; }

    public virtual LoginModel Login { get; set; } = null!;

    public bool ValidateEmail()
    {
        return EmailValidationRegex().IsMatch(Login.Email);
    }

    public bool ValidatePassword()
    {
        return PasswordValidationRegex().IsMatch(Login.Password);
    }

    [GeneratedRegex(@"^[\w\.\-]+@([\w\-]+\.)+[\w\-]{2,}$", RegexOptions.CultureInvariant)]
    private static partial Regex EmailValidationRegex();

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", RegexOptions.CultureInvariant)]
    private static partial Regex PasswordValidationRegex();

    public UserDto ToUserDto() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        FirstName = FirstName,
        LastName = LastName,
        Role = Role,
        Email = Login.Email,
        IsActive = IsActive
    };
}