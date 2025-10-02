
using Microsoft.AspNetCore.Mvc;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Persistence;

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

    internal static async Task GetAllNoteTypes([FromServices] NoteAppBackendContext context)
    {
        throw new NotImplementedException();
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
