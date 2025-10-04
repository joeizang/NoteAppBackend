using NodaTime;

namespace NoteAppBackend.Kernel.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    public Instant CreatedAt { get; set; }

    public Instant UpdatedAt { get; set; }

    protected BaseEntity(Guid? id)
    {
        Id = id ?? Ulid.NewUlid(DateTimeOffset.Now).ToGuid();
        CreatedAt = Instant.FromDateTimeUtc(DateTime.Now);
        UpdatedAt = CreatedAt;
    }

    protected BaseEntity() { }
}
