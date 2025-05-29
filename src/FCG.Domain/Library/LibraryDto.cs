using FCG.Domain.Profile;

namespace FCG.Domain.Library;

public class LibraryDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Wishlist { get; set; }

    public UserDto User { get; set; } = null!;
    public LibraryItemDto Item { get; set; } = null!;
}