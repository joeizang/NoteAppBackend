namespace NoteAppBackend.DomainModels.DataTransferObjects;

public record NoteCreationDto(string NoteTitle, string NoteBody, string Date, string Others);

public record NoteSummaryDto(Guid NoteId, string Title, string NoteDate, NoteTypeSummary TypeSummary);

public record NoteTypeSummary(Guid TypeId, string TypeName, string TypeColorCode);

