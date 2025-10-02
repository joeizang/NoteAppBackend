namespace NoteAppBackend.Kernel.Common;

public abstract class AggregateRoot : BaseEntity
{
    protected AggregateRoot(Guid? id) : base(id ?? Ulid.NewUlid().ToGuid())
    {
        
    }

    protected AggregateRoot() { }
}
