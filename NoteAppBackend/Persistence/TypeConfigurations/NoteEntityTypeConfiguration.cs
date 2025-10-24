using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteAppBackend.DomainModels;

namespace NoteAppBackend.Persistence.TypeConfigurations;

public sealed class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        // builder.HasOne(n => n.Type)
        //     .WithOne()
        //     .IsRequired()
        //     .OnDelete(DeleteBehavior.NoAction);
        // builder.HasOne(n => n.NoteOwner)
        //     .WithMany(o => o.UserNotes)
        //     .HasForeignKey(n => n.NoteOwnerId)
        //     .OnDelete(DeleteBehavior.NoAction);

        builder.Property(n => n.NoteTypeId)
            .IsRequired();

        builder.Property(n => n.NoteTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.NoteBody)
            .HasMaxLength(2500)
            .IsRequired();

        builder.Property(n => n.NoteDate)
            .IsRequired();


        builder.HasIndex(n => n.NoteTitle);
        builder.HasIndex(n => n.NoteBody);
        builder.HasIndex(n => n.CreatedAt);
        builder.HasIndex(n => n.NoteDate);
    }
}
