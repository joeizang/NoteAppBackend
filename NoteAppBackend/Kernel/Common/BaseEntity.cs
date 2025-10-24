namespace NoteAppBackend.Kernel.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public bool IsDeleted { get; set; }

    // public DateOnly CreatedOn { get; set; }
    public DateTime CreatedAt { get; set; }
    // public DateOnly UpdatedOn { get; set; }
    public DateTime UpdatedAt { get; set; }

    protected BaseEntity(Guid? id)
    {
        Id = id ?? Ulid.NewUlid(DateTimeOffset.Now).ToGuid();
        CreatedAt = DateTime.Now;
        // CreatedAt = TimeOnly.FromDateTime(DateTime.Today);
        UpdatedAt = CreatedAt;
        // UpdatedOn = CreatedOn;
    }

    protected BaseEntity() { }
}
