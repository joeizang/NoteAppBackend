using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteAppBackend.DomainModels;

namespace NoteAppBackend.Persistence;

public sealed class NoteAppBackendContext : IdentityDbContext
{
    public NoteAppBackendContext(DbContextOptions<NoteAppBackendContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(NoteAppBackendContext).Assembly);
        base.OnModelCreating(builder);
    }

    public DbSet<Note> Notes { get; set; }

    public DbSet<NoteType> NoteTypes { get; set; }

}
