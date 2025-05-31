using FCG.Domain.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Data.Configuration;

public class LoginDbConfiguration : IEntityTypeConfiguration<LoginModel>
{
    public void Configure(EntityTypeBuilder<LoginModel> builder)
    {
        builder.ToTable("Logins");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.CreatedAt);
        builder.Property(l => l.UpdatedAt);

        builder.Property(l => l.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(l => l.Email).IsUnique();
        builder.Property(l => l.Password).IsRequired().HasMaxLength(100);

        // Admin login creation
        builder.HasData(new LoginModel
        {
            Id = Guid.Parse("E402588B-C931-437D-83AC-941010840391"),
            Email = "admin@email.com",
            Password = "AQAAAAIAAYagAAAAEFvxevg2Oz/DiArPRXiGGbDy29yxw2ZrroJzNh68WHeARuCaupa6cqkHRj2x0B2S8A==",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }
}
