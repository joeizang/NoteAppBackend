namespace NoteAppBackend.DomainModels.DataTransferObjects;

public record NoteCreationDto(string NoteTitle, string NoteBody, string Date, string Others);

public record NoteSummaryDto(Guid NoteId, string Title, string NoteDate, NoteTypeSummaryDto TypeSummary);

public record NoteTypeSummaryDto(Guid TypeId, string TypeName, string TypeColorCode);

public record NotePagedSummary();

public record NoteDto();


public static class DtoExtensions
{
    extension(Note note)
    {
        public NoteSummaryDto MapNoteToNoteSummaryDto() =>
            new NoteSummaryDto(note.Id, note.NoteTitle, note.NoteDate.ToString(),
                new NoteTypeSummaryDto(note.Type.Id, note.Type.Name, note.Type.ColorCode));

        public NotePagedSummary MaptNoteToNotePagedSummaryDto() => new();

        public NoteDto MapNoteToNoteDto() => new();
    }
}

