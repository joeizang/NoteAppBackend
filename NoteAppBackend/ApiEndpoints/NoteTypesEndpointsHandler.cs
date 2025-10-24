
using Microsoft.AspNetCore.Mvc;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Persistence;
using NoteAppBackend.Persistence.PersistenceServices;
using LanguageExt.Common;
using NoteAppBackend.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace NoteAppBackend.ApiEndpoints;

public static class NoteTypesEndpointHandler
{
    internal static async Task<IResult> CreateNoteType([FromServices] NoteAppBackendContext context,
        [FromBody] NoteTypeCreationDto dto, [FromServices] ICommandService command, CancellationToken token)
    {
        var noteType = NoteType.Create(dto);
        var result = await command.CreateNoteType(noteType).ConfigureAwait(false);
        return result.Match<IResult>(
            (r) => TypedResults.Ok(new NoteTypeSummaryDto(r.Id, r.Name, r.Description, r.ColorCode)),
            (e) => TypedResults.BadRequest(e.Message)
        );
    }

    internal static async Task<IResult> DeleteNoteType([FromServices] NoteAppBackendContext db, Guid id)
    {
        try
        {
            var note = await db.NoteTypes.SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
            if (note is null) return TypedResults.NotFound();
            db.NoteTypes.Remove(note);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest("Deletion Failed!");
        }
    }

    internal static IResult GetAllNoteTypes([FromServices] NoteAppBackendContext context)
    {
        // var result = NotesQueryService.GetNoteAllTypes(context);
        var result = context.NoteTypes.AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new { t.Id, t.Name, t.Description, t.ColorCode })
            .Take(10)
            .ToArray();
        return result.Length < 1 ? TypedResults.Ok<List<NoteSummaryDto>>([]) : TypedResults.Ok(result);
    }

    // internal static async Task GetPagedNoteTypes([FromServices] NoteAppBackendContext context, string cursor)
    // {
    //     throw new NotImplementedException();
    // }

    internal static IResult GetTypesById([FromServices] NoteAppBackendContext context, Guid id)
    {
        var result = NotesQueryService.GetNoteTypeById(context, id);
        return result is null ? TypedResults.NotFound(id) : TypedResults.Ok(result);
    }

    // internal static async Task PatchNoteType([FromServices] NoteAppBackendContext context, [FromBody] Guid id)
    // {
    //     throw new NotImplementedException();
    // }

    // internal static async Task UpdateNoteType(HttpContext context, [FromBody] Guid id)
    // {
    //     throw new NotImplementedException();
    // }
}
