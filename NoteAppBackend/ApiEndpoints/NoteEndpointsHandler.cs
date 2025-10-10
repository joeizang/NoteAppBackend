
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NoteAppBackend.DomainModels;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Persistence;
using NoteAppBackend.Persistence.PersistenceServices;

namespace NoteAppBackend.ApiEndpoints;

public static class NoteEndpointsHandler
{
    internal static async Task<IResult> CreateNote([FromServices] NoteAppBackendContext context,
        [FromBody] NoteCreationDto dto, [FromServices] ICommandService<Note> command)
    {
        var note = Note.Create(dto);
        var result = await command.Create(note).ConfigureAwait(false);
        return result.Match<IResult>(
            (r) => TypedResults.Ok(r),
            (e) => TypedResults.BadRequest(e.Message));
    }

    internal static async Task<IResult> DeleteNote([FromServices] NoteAppBackendContext context,
        [FromServices] ICommandService<Note> command, Guid noteId)
    {
        var result = await command.Delete(noteId).ConfigureAwait(false);
        return result.Match<IResult>((r) => TypedResults.Ok() , (e) => TypedResults.BadRequest(e.Message));
    }

    internal static IResult GetAllNotes([FromServices] NoteAppBackendContext context)
        => TypedResults.Ok(NotesQueryService.GetAllNotes(context));

    internal static IResult GetNoteById([FromServices] NoteAppBackendContext context, Guid id)
    {
        var result = NotesQueryService.GetNoteById(context, id);
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    internal static IResult GetPagedNotes([FromServices] NoteAppBackendContext context, string cursor)
    {
        var parseResult = DateTime.TryParse(cursor, out var cursorInstant);
        if (parseResult == false) return TypedResults.BadRequest($"{cursor} is not a valid cursor!");
        var result = NotesQueryService.GetPagedNotes(context, Instant.FromDateTimeUtc(cursorInstant));
        return result is null ? TypedResults.Ok<List<NotePagedSummary>>([]) : TypedResults.Ok(result);
    }

    internal static async Task<IResult> UpdateNote([FromServices] NoteAppBackendContext context,
        [FromBody] NoteUpdateDto dto, [FromServices] ICommandService<Note> command)
    {
        var entity = NoteUpdateDto.MapToNote(dto);
        if (entity.Item1 is null && entity.Item2 is not null)
            return TypedResults.BadRequest(entity.Item2.ErrorMessage);

        var result = await command.Update(entity.Item1!).ConfigureAwait(false);

        return result.Match<IResult>(
            (r) => TypedResults.Ok(r.MapNoteToNoteSummaryDto()), (e) => TypedResults.InternalServerError(e.Message));
    }

    internal static IResult GetNoteTypes([FromServices] NoteAppBackendContext context)
    {
        var result = NotesQueryService.GetNoteTypes(context);
        return result is null ? TypedResults.Ok<IEnumerable<NoteTypeSummaryDto>>([]) : TypedResults.Ok(result);
    }
}
