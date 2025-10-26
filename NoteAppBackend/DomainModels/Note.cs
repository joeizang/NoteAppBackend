using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.DomainModels;

public sealed class Note : AggregateRoot
{
    private Note(Guid? id) : base(id ?? Ulid.NewUlid().ToGuid()) { }

    private Note() { }

    public string NoteTitle { get; set; } = string.Empty;

    public string NoteBody { get; set; } = string.Empty;

    public DateOnly NoteDate { get; set; } = default!;

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
            NoteDate = DateOnly.FromDateTime(dtoDate),
            // NoteOwnerId = Ulid.NewUlid().ToGuid().ToString(),
            CreatedOn = DateOnly.FromDateTime(currentOpDate),
            UpdatedOn = DateOnly.FromDateTime(currentOpDate),
            CreatedAt = TimeOnly.FromDateTime(currentOpDate),
            UpdatedAt = TimeOnly.FromDateTime(currentOpDate)
        };
    }

    public static Note UpdateNote(NoteUpdateDto dto)
    {
        var now = DateTime.Parse(dto.UpdateDate);
        return new Note
        {
            Id = dto.NoteId,
            NoteTitle = dto.Title,
            NoteBody = dto.NoteBody,
            NoteTypeId = dto.NoteTypeId,
            UpdatedAt = TimeOnly.FromDateTime(now)
        };
    }
}
