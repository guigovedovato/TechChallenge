using FCG.Domain.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Data.Configuration;

public class LibraryDbConfiguration : IEntityTypeConfiguration<LibraryModel>
{
    public void Configure(EntityTypeBuilder<LibraryModel> builder)
    {
        builder.ToTable("Library");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.CreatedAt);
        builder.Property(l => l.UpdatedAt);

        //builder.Property(l => l.UserId).IsRequired();
        builder.Property(l => l.LibraryItemModelId).IsRequired();
    }
}
