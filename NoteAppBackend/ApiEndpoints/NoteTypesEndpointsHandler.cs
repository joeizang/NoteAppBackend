
using Microsoft.AspNetCore.Mvc;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Persistence;
using NoteAppBackend.Persistence.PersistenceServices;
using LanguageExt.Common;

namespace NoteAppBackend.ApiEndpoints;

public static class NoteTypesEndpointHandler
{
    internal static async Task CreateNoteType([FromServices] NoteAppBackendContext context,
        [FromBody] NoteCreationDto dto)
    {
        throw new NotImplementedException();
    }

    internal static async Task DeleteNoteType([FromServices] NoteAppBackendContext context, Guid id)
    {
        throw new NotImplementedException();
    }

    internal static async Task<Result<List<NoteSummaryDto>>> GetAllNoteTypes([FromServices] NoteAppBackendContext context)
    {
        var result = NotesQueryService.GetAllNotes(context);
        return new Result<List<NoteSummaryDto>>(result);
    }

    internal static async Task GetPagedNoteTypes([FromServices] NoteAppBackendContext context, string cursor)
    {
        throw new NotImplementedException();
    }

    internal static async Task GetTypesById([FromServices] NoteAppBackendContext context, Guid id)
    {
        throw new NotImplementedException();
    }

    internal static async Task PatchNoteType([FromServices] NoteAppBackendContext context, [FromBody] Guid id)
    {
        throw new NotImplementedException();
    }

    internal static async Task UpdateNoteType(HttpContext context, [FromBody] Guid id)
    {
        throw new NotImplementedException();
    }
}
