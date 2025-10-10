namespace NoteAppBackend.DomainModels.DataTransferObjects;

public record NoteCreationDto(string NoteTitle, string NoteBody, string Date, NoteTypeCreationDto TypeDto);

public record NoteSummaryDto(Guid NoteId, string Title, string NoteDate, NoteTypeSummaryDto TypeSummary);

public record NoteTypeSummaryDto(Guid TypeId, string TypeName, string Description, string TypeColorCode);

public record NotePagedSummary(Guid NoteId, string Title, string NoteDate, NoteTypeSummaryDto TypeSummary);

public record NoteDto(Guid NoteId, string Title, string NoteBody,
    NoteTypeSummaryDto TypeSummary, string UpdatedAt);

public record NoteUpdateDto(Guid NoteId, string Title, string NoteBody, 
    string CurrentDate, NoteTypeSummaryDto TypeSummary);


public static class DtoExtensions
{
    extension(Note note)
    {
        public NoteSummaryDto MapNoteToNoteSummaryDto() =>
            new NoteSummaryDto(note.Id, note.NoteTitle, note.NoteDate.ToString(),
                new NoteTypeSummaryDto(note.Type.Id, note.Type.Name, note.Type.Description, note.Type.ColorCode));

        public NotePagedSummary MaptNoteToNotePagedSummaryDto() => new(note.Id, note.NoteTitle, note.NoteDate.ToString(),
                new NoteTypeSummaryDto(note.Type.Id, note.Type.Name, note.Type.Description, note.Type.ColorCode));

        public NoteDto MapNoteToNoteDto() => new(note.Id, note.NoteTitle, note.NoteBody,
            new NoteTypeSummaryDto(note.Type.Id, note.Type.Name, note.Type.Description, note.Type.ColorCode),
            note.UpdatedAt.ToString());
    }

    extension(NoteUpdateDto)
    {
        public static (Note?, ValidationError?) MapToNote(NoteUpdateDto dto)
        {
            if (dto is null)
                return (null, new ValidationError("Your dto is in an invalid state!"));
            if (string.IsNullOrEmpty(dto.Title) && string.IsNullOrEmpty(dto.NoteBody) 
                && string.IsNullOrEmpty(dto.CurrentDate) && dto.TypeSummary is not null)
                return (null, new ValidationError("Your dto is in an invalid state! Some required fields are invalid!"));
            return (Note.UpdateNote(dto), null);
        }
    }
}

public record ValidationError(string ErrorMessage);

public record NoteTypeCreationDto(string TypeName, string TypeDescription, string ColorCode);
