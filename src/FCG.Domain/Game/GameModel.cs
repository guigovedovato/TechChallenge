using FCG.Domain.Common;

namespace FCG.Domain.Game;

public record GameModel : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string ImageUrl { get; set; } = null!;
    public Genre Genre { get; set; }
    public bool IsDemo { get; set; }
    public required string Publisher { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;

    public GameDto ToGameDto() => new()
    {
        Id = Id,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        Genre = Genre,
        IsDemo = IsDemo,
        Publisher = Publisher,
        ReleaseDate = ReleaseDate,
        Price = Price,
        IsActive = IsActive
    };
}