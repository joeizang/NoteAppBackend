
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteAppBackend.DomainModels;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Persistence;
using NoteAppBackend.Persistence.PersistenceServices;

namespace NoteAppBackend.ApiEndpoints;

public static class NoteEndpointsHandler
{
    internal static async Task<IResult> CreateNote([FromServices] NoteAppBackendContext context,
        [FromBody] NoteCreationDto dto, [FromServices] ICommandService command)
    {
        var note = Note.Create(dto);
        var result = await command.Create(note).ConfigureAwait(false);
        return result.Match<IResult>(
            (r) => TypedResults.Ok(r),
            (e) => TypedResults.BadRequest(e.Message));
    }

    internal static async Task<IResult> DeleteNote(Guid id, [FromServices] NoteAppBackendContext context,
        [FromServices] ICommandService command)
    {
        try
        {
            var note = await context.Notes.SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (note is null) return TypedResults.NotFound();
            context.Notes.Remove(note);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest("Deletion Failed!");
        }
    }

    internal static async Task<IResult> GetAllNotes([FromServices] NoteAppBackendContext context,
        CancellationToken token)
    {
        var result = await context.Notes.AsNoTracking()
                .OrderByDescending(n => n.Id)
                .Select(n => n.MapNoteToNoteSummaryDto())
                .ToListAsync(token).ConfigureAwait(false);
        return result.Count < 1 ? TypedResults.Ok<List<NoteSummaryDto>>([]) : TypedResults.Ok(result);
    }

    internal static IResult GetNoteById([FromServices] NoteAppBackendContext context, Guid id)
    {
        var result = NotesQueryService.GetNoteById(context, id);
        var typedNote = NotesQueryService.GetNoteTypeById(context, result!.NoteTypeId);
        var combined = new { Note = result!, NoteType = typedNote };
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(combined);
    }

    internal static IResult GetPagedNotes([FromServices] NoteAppBackendContext context, [FromQuery] string cursor)
    {
        var treated = Encoding.UTF8.GetString(Convert.FromBase64String(cursor));
        Console.WriteLine(treated);
        var whatParts = treated.Split("|");
        // Console.WriteLine(whatParts);
        return TypedResults.NoContent();
        var parseResult = DateTime.TryParse(cursor, out var cursorInstant);
        if (parseResult == false) return TypedResults.BadRequest($"{cursor} is not a valid cursor!");
        var result = NotesQueryService.GetPagedNotes(context, cursorInstant);
        return result is null ? TypedResults.Ok<List<NotePagedSummary>>([]) : TypedResults.Ok(result);
    }

    internal static async Task<IResult> UpdateNote([FromServices] NoteAppBackendContext context,
        [FromBody] NoteUpdateDto dto, [FromServices] ICommandService command)
    {
        var entity = NoteUpdateDto.MapToNote(dto);
        if (entity.Item1 is null && entity.Item2 is not null)
            return TypedResults.BadRequest(entity.Item2.ErrorMessage);

        var result = await command.Update(dto).ConfigureAwait(false);

        return result.Match<IResult>(
            (r) => TypedResults.Ok(), (e) => TypedResults.InternalServerError(e.Message));
    }

    internal static IResult GetNoteTypes([FromServices] NoteAppBackendContext context)
    {
        var result = NotesQueryService.GetNoteTypes(context);
        return result is null ? TypedResults.Ok<IEnumerable<NoteTypeSummaryDto>>([]) : TypedResults.Ok(result);
    }

    internal static async Task<IResult> CreateNoteType([FromServices] NoteAppBackendContext context,
    [FromBody] NoteTypeCreationDto dto, [FromServices] ICommandService command)
    {
        if (dto is null)
            return TypedResults.BadRequest("Your request is invalid, the request valid is emtpy!");
        if (string.IsNullOrEmpty(dto.ColorCode) && string.IsNullOrEmpty(dto.TypeName))
            return TypedResults.BadRequest("Your request is invalid, missing required properties");
        var result = await command.CreateNoteType(NoteType.Create(dto)).ConfigureAwait(false);
        return result.Match<IResult>(
            (r) => TypedResults.Ok(r.MapToNoteTypeSummaryDto()),
            (e) => TypedResults.InternalServerError("The note type could not be created because of an error!") //logging?
        );
    }
}
