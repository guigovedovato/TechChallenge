using MessagePack;

namespace FCG.Domain.Library;

[MessagePackObject]
public class LibraryItemResponse(int GameId, string GameName)
{
    [Key(0)]
    public int GameId { get; set; } = GameId;

    [Key(1)]
    public string GameName { get; set; } = GameName;
}