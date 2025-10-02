using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.DomainModels;

public sealed class NoteType : BaseEntity
{
    private NoteType(Guid? id) : base(id ?? Ulid.NewUlid().ToGuid()) { }

    private NoteType() { }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;
}
