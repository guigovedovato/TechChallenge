using FCG.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Data.Configuration;

public class UserDbConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.CreatedAt);
        builder.Property(u => u.UpdatedAt);

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Role).IsRequired().HasConversion<string>();
        builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(u => u.LoginId).IsRequired();

        // Relationships
        builder.HasOne(c => c.Login)
            .WithOne(c => c.User)
            .HasForeignKey<UserModel>(c => c.LoginId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Admin user creation
        builder.HasData(new UserModel
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "User",
            Role = Role.Admin,
            IsActive = true,
            LoginId = Guid.Parse("E402588B-C931-437D-83AC-941010840391"),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
    }
}
