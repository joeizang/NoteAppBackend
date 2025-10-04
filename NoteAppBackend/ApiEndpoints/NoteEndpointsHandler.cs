
using Microsoft.AspNetCore.Mvc;
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
        return TypedResults.Ok(result);
    }

    internal static async Task DeleteNote([FromServices] NoteAppBackendContext context)
    {
        throw new NotImplementedException();
    }

    internal static async Task GetAllNotes([FromServices] NoteAppBackendContext context)
    {
        throw new NotImplementedException();
    }

    internal static async Task GetNoteById([FromServices] NoteAppBackendContext context)
    {
        throw new NotImplementedException();
    }

    internal static async Task GetPagedNotes([FromServices] NoteAppBackendContext context)
    {
        throw new NotImplementedException();
    }

    internal static async Task UpdateNote([FromServices] NoteAppBackendContext context,
        [FromBody] NoteUpdateDto dto, [FromServices] ICommandService<Note> command)
    {
        var result = 
    }
}
