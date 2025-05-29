using MessagePack;

namespace FCG.Domain.Library;

[MessagePackObject]
public class LibraryResponse(IEnumerable<LibraryItemResponse> LibraryItems, int TotalCount)
{
    [Key(0)]
    public IEnumerable<LibraryItemResponse> LibraryItems { get; set; } = LibraryItems;

    [Key(1)]
    public int TotalCount { get; set; } = TotalCount;
}