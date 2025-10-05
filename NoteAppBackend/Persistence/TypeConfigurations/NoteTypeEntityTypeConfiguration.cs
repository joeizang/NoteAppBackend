using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteAppBackend.DomainModels;

namespace NoteAppBackend.Persistence.TypeConfigurations;

public sealed class NoteTypeEntityTypeConfiguration : IEntityTypeConfiguration<NoteType>
{
    public void Configure(EntityTypeBuilder<NoteType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.ColorCode);
        builder.HasIndex(x => x.CreatedAt);
    }
}
