using NodaTime;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.DomainModels;

public sealed class Note : AggregateRoot
{
    private Note(Guid? id) : base(id ?? Ulid.NewUlid().ToGuid()) { }

    private Note() { }

    public string NoteTitle { get; set; } = string.Empty;

    public string NoteBody { get; set; } = string.Empty;

    public Instant NoteDate { get; set; } = default!;

    public NoteType Type { get; set; } = null!;

    public Guid NoteTypeId { get; set; }

    public List<string> Media { get; set; } = [];

    public static Note Create(NoteCreationDto dto)
    {
        return new Note();
    }
}
