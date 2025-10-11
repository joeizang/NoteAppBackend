using NodaTime;

namespace NoteAppBackend.Kernel.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateOnly CreatedOn { get; set; }
    public TimeOnly CreatedAt { get; set; }
    public DateOnly UpdatedOn { get; set; }
    public TimeOnly UpdatedAt { get; set; }

    protected BaseEntity(Guid? id)
    {
        Id = id ?? Ulid.NewUlid(DateTimeOffset.Now).ToGuid();
        CreatedOn = DateOnly.FromDateTime(DateTime.Today);
        CreatedAt = TimeOnly.FromDateTime(DateTime.Today);
        UpdatedAt = CreatedAt;
        UpdatedOn = CreatedOn;
    }

    protected BaseEntity() { }
}
