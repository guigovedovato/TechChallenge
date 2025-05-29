using FCG.Domain.Common;
using FCG.Domain.Profile;

namespace FCG.Domain.Library;

public record LibraryModel : EntityBase
{
    public bool Wishlist { get; set; }

    //public int UserId { get; set; }
    public int LibraryItemModelId { get; set; }

    //public virtual UserModel User { get; set; } = null!;
    public virtual LibraryItemModel Item { get; set; } = null!;
}