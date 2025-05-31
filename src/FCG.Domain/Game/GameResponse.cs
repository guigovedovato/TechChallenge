using MessagePack;

namespace FCG.Domain.Game;

[MessagePackObject]
public class GameResponse(Guid Id, DateTime CreatedAt, DateTime UpdatedAt, string Name, string Description, string ImageUrl, Genre Genre, bool IsDemo, string Publisher, DateTime ReleaseDate, decimal Price, bool IsActive)
{
    [Key(0)]
    public Guid Id { get; set; } = Id;

    [Key(1)]
    public DateTime CreatedAt { get; set; } = CreatedAt;

    [Key(2)]
    public DateTime UpdatedAt { get; set; } = UpdatedAt;

    [Key(3)]
    public string Name { get; set; } = Name;

    [Key(4)]
    public string Description { get; set; } = Description;

    [Key(5)]
    public string ImageUrl { get; set; } = ImageUrl;

    [Key(6)]
    public Genre Genre { get; set; } = Genre;

    [Key(7)]
    public bool IsDemo { get; set; } = IsDemo;

    [Key(8)]
    public string Publisher { get; set; } = Publisher;

    [Key(9)]
    public DateTime ReleaseDate { get; set; } = ReleaseDate;

    [Key(10)]
    public decimal Price { get; set; } = Price;

    [Key(11)]
    public bool IsActive { get; set; } = IsActive;
}