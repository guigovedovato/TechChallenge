using FCG.Domain.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Data.Configuration;

public class GameDbConfiguration : IEntityTypeConfiguration<GameModel>
{
    public void Configure(EntityTypeBuilder<GameModel> builder)
    {
        builder.ToTable("Games");
        builder.HasKey(g => g.Id);
        builder.Property(u => u.CreatedAt);
        builder.Property(u => u.UpdatedAt);

        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
        builder.Property(g => g.Description).HasMaxLength(500);
        builder.Property(g => g.ImageUrl).HasMaxLength(200);
        builder.Property(g => g.Genre).IsRequired();
        builder.Property(g => g.IsDemo).IsRequired();
        builder.Property(g => g.Publisher).IsRequired().HasMaxLength(100);
        builder.Property(g => g.ReleaseDate).IsRequired();
        builder.Property(g => g.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(g => g.IsActive).IsRequired();
    }
}
