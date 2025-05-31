using FCG.Domain.Game;

namespace FCG.Domain.Library;

public class LibraryItemDto
{
    public int Id { get; set; }
    public IEnumerable<GameDto> Games { get; set; } = null!;
}