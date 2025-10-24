namespace NoteAppBackend.ApiEndpoints;

public static class NoteEndpoints
{
    public static IEndpointRouteBuilder MapNotesEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("/api/notes");

        group.MapGet("/all", NoteEndpointsHandler.GetAllNotes);
        group.MapGet("/{id:guid}", NoteEndpointsHandler.GetNoteById);
        group.MapGet("/{cursor}", NoteEndpointsHandler.GetPagedNotes);

        group.MapPost("", NoteEndpointsHandler.CreateNote);
        group.MapPut("/{id:guid}", NoteEndpointsHandler.UpdateNote);
        // group.MapPatch("/{id:guid}", NoteEndpointsHandler.PatchNote);
        group.MapDelete("/{id:guid}", NoteEndpointsHandler.DeleteNote);

        return group;
    }
}


public static class NoteTypeEndpoints
{
    public static IEndpointRouteBuilder MapNoteTypeEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("/api/notetypes");

        group.MapGet("/all", NoteTypesEndpointHandler.GetAllNoteTypes);
        // group.MapGet("", NoteTypesEndpointHandler.GetPagedNoteTypes);
        group.MapGet("/{id:guid}", NoteTypesEndpointHandler.GetTypesById);

        group.MapPost("", NoteTypesEndpointHandler.CreateNoteType);
        // group.MapPut("/{id:guid}", NoteTypesEndpointHandler.UpdateNoteType);
        // group.MapPatch("/{id:guid}", NoteTypesEndpointHandler.PatchNoteType);
        group.MapDelete("/{id:guid}", NoteTypesEndpointHandler.DeleteNoteType);

        return group;
    }
}
