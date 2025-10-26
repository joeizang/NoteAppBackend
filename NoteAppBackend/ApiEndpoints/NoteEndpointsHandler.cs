
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteAppBackend.DomainModels;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Helpers;
using NoteAppBackend.Persistence;
using NoteAppBackend.Persistence.PersistenceServices;
using ZLinq;

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

    internal static IResult GetPagedNotes([FromServices] NoteAppBackendContext context, string cursor)
    {
        var treated = NoteAppHelper.Decode(cursor);
        Console.WriteLine(treated);

        var result = NotesQueryService.GetPagedNotes(context, treated);
        return result is null ? TypedResults.Ok<List<NotePagedSummary>>([]) : TypedResults.Ok(result);
    }

    internal static async Task<IResult> GetNoteBySearchParameter([FromServices] NoteAppBackendContext context,
        string searchParam)
    {
        var result = NotesQueryService.GetNoteBySearchParameter(context, searchParam);
        // var result = await context.Notes.AsNoTracking()
        //     .Where(n => EF.Functions.Like(n.NoteBody, $"%{searchParam}%") ||
        //         EF.Functions.Like(n.NoteTitle, $"%{searchParam}%"))
        //     .Select(n => new { NoteId = n.Id, Title = n.NoteTitle })
        //     .Take(2)
        //     .ToListAsync().ConfigureAwait(false);
        return result is null ? TypedResults.Ok<List<NoteDto>>([]) : TypedResults.Ok(result);
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
