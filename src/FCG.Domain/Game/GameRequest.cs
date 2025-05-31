namespace FCG.Domain.Game;

public class GameRequest(string Name, string Description, string ImageUrl, Genre Genre, string Publisher, DateTime ReleaseDate, decimal Price, bool IsDemo = false)
{
    public string Name { get; set; } = Name;

    public string Description { get; set; } = Description;

    public string ImageUrl { get; set; } = ImageUrl;

    public Genre Genre { get; set; } = Genre;

    public string Publisher { get; set; } = Publisher;

    public DateTime ReleaseDate { get; set; } = ReleaseDate;

    public decimal Price { get; set; } = Price;

    public bool IsDemo { get; set; } = IsDemo;

    public GameDto ToGameDto() => new()
    {
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        Genre = Genre,
        Publisher = Publisher,
        ReleaseDate = ReleaseDate,
        Price = Price,
        IsDemo = IsDemo
    };
}