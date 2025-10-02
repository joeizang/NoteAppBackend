using NodaTime;

namespace NoteAppBackend.Kernel.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public Instant CreatedAt { get; set; }

    public Instant UpdatedAt { get; set; }

    protected BaseEntity(Guid? id)
    {
        Id = id ?? Ulid.NewUlid(DateTimeOffset.Now).ToGuid();
    }

    protected BaseEntity() { }
}
