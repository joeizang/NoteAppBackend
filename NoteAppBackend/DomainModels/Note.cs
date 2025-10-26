using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.DomainModels;

public sealed class Note : AggregateRoot
{
    private Note(Guid? id) : base(id ?? Ulid.NewUlid().ToGuid()) { }

    private Note() { }

    public string NoteTitle { get; set; } = string.Empty;

    public string NoteBody { get; set; } = string.Empty;

    public DateTime NoteDate { get; set; } = default!;

    public Guid NoteTypeId { get; set; }

    public NoteType NoteType { get; set; } = null!;

    // public ApplicationUser NoteOwner { get; set; }

    // public string NoteOwnerId { get; set; } = string.Empty;

    public List<string> Media { get; set; } = [];

    public static Note Create(NoteCreationDto dto)
    {
        var dtoDate = DateTime.Parse(dto.Date);
        var currentOpDate = DateTime.Now;
        return new Note
        {
            NoteTitle = dto.NoteTitle,
            NoteBody = dto.NoteBody,
            NoteTypeId = dto.NoteTypeId,
            NoteDate = DateTime.SpecifyKind(dtoDate, DateTimeKind.Utc),
            // NoteOwnerId = Ulid.NewUlid().ToGuid().ToString(),
            CreatedAt = currentOpDate,
            UpdatedAt = currentOpDate
        };
    }

    public static Note UpdateNote(NoteUpdateDto dto)
    {
        return new Note
        {
            Id = dto.NoteId,
            NoteTitle = dto.Title,
            NoteBody = dto.NoteBody,
            NoteTypeId = dto.NoteTypeId,
            UpdatedAt = DateTime.Now
        };
    }
}
