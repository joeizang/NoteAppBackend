using NoteAppBackend.Kernel.Helpers;

namespace NoteAppBackend.DomainModels.DataTransferObjects;

public record NoteCreationDto(string NoteTitle, string NoteBody, string Date, Guid NoteTypeId);

public record NoteSummaryDto(Guid NoteId, string Title, string NoteDate, Guid NoteTypeId, string Cursor);

public record NoteTypeSummaryDto(Guid TypeId, string TypeName, string Description, string TypeColorCode,
    string Cursor);

public record NotePagedSummary(Guid NoteId, string Title, string NoteDate, string ColorCode, string Cursor);

public record NoteDto(Guid NoteId, string Title, string NoteBody,
    Guid NoteTypeId, string UpdatedAt);

public record NoteUpdateDto(Guid NoteId, string Title, string NoteBody, List<string> Media, Guid NoteTypeId, string UpdateDate);


public static class DtoExtensions
{
    extension(Note note)
    {
        public NoteSummaryDto MapNoteToNoteSummaryDto() =>
            new NoteSummaryDto(note.Id, note.NoteTitle, note.NoteDate.ToString(), note.NoteTypeId,
            NoteAppHelper.Encode(note.Id));

        public NoteSummaryDto MaptNoteToNotePagedSummaryDto() => new(note.Id, note.NoteTitle, note.NoteDate.ToString(), note.NoteTypeId, NoteAppHelper.Encode(note.Id));

        public NoteDto MapNoteToNoteDto() => new(note.Id, note.NoteTitle, note.NoteBody,
            note.NoteTypeId, note.UpdatedAt.ToString());
    }

    extension(NoteUpdateDto)
    {
        public static (Note?, ValidationError?) MapToNote(NoteUpdateDto dto)
        {
            if (dto is null)
                return (null, new ValidationError("Your dto is in an invalid state!"));
            if (string.IsNullOrEmpty(dto.Title) && string.IsNullOrEmpty(dto.NoteBody))
                return (null, new ValidationError("Your dto is in an invalid state! Some required fields are invalid!"));
            return (Note.UpdateNote(dto), null);
        }
    }
    
    extension(NoteType noteType)
    {
        public NoteTypeSummaryDto MapToNoteTypeSummaryDto() =>
            new(noteType.Id, noteType.Name, noteType.Description, noteType.ColorCode,
            NoteAppHelper.Encode(noteType.Id));   
    }
}

public record ValidationError(string ErrorMessage);

public record NoteTypeCreationDto(string TypeName, string TypeDescription, string ColorCode);
