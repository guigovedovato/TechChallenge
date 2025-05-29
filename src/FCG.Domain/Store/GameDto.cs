using FCG.Domain.Library;

namespace FCG.Domain.Store;

public class GameDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string ImageUrl { get; set; } = null!;
    public Genre Genre { get; set; }
    public bool IsDemo { get; set; }
    public required string Publisher { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    
    public LibraryDto Library { get; set; } = null!;

    public GameModel ToGameModel() => new()
    {
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        Genre = Genre,
        IsDemo = IsDemo,
        Publisher = Publisher,
        ReleaseDate = ReleaseDate,
        Price = Price
    };

    public GameResponse ToGameResponse() => new(Id, CreatedAt, UpdatedAt, Name, Description, ImageUrl, Genre, IsDemo, Publisher, ReleaseDate, Price, IsActive);
}