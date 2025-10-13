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

    public DateOnly NoteDate { get; set; } = default!;

    public NoteType Type { get; set; } = null!;

    public Guid NoteTypeId { get; set; }

    public ApplicationUser NoteOwner { get; set; }

    public string NoteOwnerId { get; set; } = string.Empty;

    public List<string> Media { get; set; } = [];

    public static Note Create(NoteCreationDto dto)
    {
        return new Note
        {
            NoteTitle = dto.NoteTitle,
            NoteBody = dto.NoteBody,
            Type = NoteType.Create(dto.TypeDto)
        };
    }

    public static Note UpdateNote(NoteUpdateDto dto)
    {
        return new Note
        {
            Id = dto.NoteId,
            NoteTitle = dto.Title,
            NoteBody = dto.NoteBody,
            UpdatedAt = TimeOnly.FromDateTime(DateTime.Parse(dto.CurrentDate))
        };
    }
}
