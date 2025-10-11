
using Microsoft.AspNetCore.Mvc;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Persistence;
using NoteAppBackend.Persistence.PersistenceServices;
using LanguageExt.Common;
using NoteAppBackend.DomainModels;

namespace NoteAppBackend.ApiEndpoints;

public static class NoteTypesEndpointHandler
{
    internal static async Task<IResult> CreateNoteType([FromServices] NoteAppBackendContext context,
        [FromBody] NoteTypeCreationDto dto, [FromServices] ICommandService<NoteType> command, CancellationToken token)
    {
        var noteType = NoteType.Create(dto);
        var result = await command.Create(noteType).ConfigureAwait(false);
        return result.Match<IResult>(
            (r) => TypedResults.Ok(new NoteTypeSummaryDto(r.Id, r.Name, r.Description, r.ColorCode)),
            (e) => TypedResults.BadRequest(e.Message)
        );
    }

    internal static async Task<IResult> DeleteNoteType([FromServices] ICommandService<NoteType> command, Guid id)
    {
        var result = await command.Delete(id).ConfigureAwait(false);
        return result.Match<IResult>(
            (r) => TypedResults.Ok(),
            (e) => TypedResults.BadRequest("Could not perform delete because NoteType was not found!")
        );
    }

    internal static IResult GetAllNoteTypes([FromServices] NoteAppBackendContext context)
    {
        var result = NotesQueryService.GetAllNotes(context);
        return result.Count < 1 ? TypedResults.Ok<List<NoteSummaryDto>>([]) : TypedResults.Ok(result);
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
