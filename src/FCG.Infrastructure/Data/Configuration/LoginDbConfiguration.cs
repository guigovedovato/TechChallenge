using FCG.Domain.Authentication;
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
    }
}
