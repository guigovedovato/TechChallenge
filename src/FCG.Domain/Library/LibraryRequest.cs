namespace FCG.Domain.Library;

public class LibraryRequest(int UserId, IEnumerable<LibraryItemRequest> LibraryItems, bool Wishlist = false)
{
    public int UserId { get; set; } = UserId;

    public IEnumerable<LibraryItemRequest> LibraryItems { get; set; } = LibraryItems;

    public bool Wishlist { get; set; } = Wishlist;
}