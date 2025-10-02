using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteAppBackend.DomainModels;

namespace NoteAppBackend.Persistence.TypeConfigurations;

public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        builder.HasOne(n => n.Type)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(n => n.NoteTitle)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(n => n.NoteBody)
            .IsRequired();

        
    }
}
