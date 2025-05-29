namespace FCG.Domain.Library;

public class LibraryItemRequest(int GameId)
{
    public int GameId { get; set; } = GameId;
}