using Microsoft.EntityFrameworkCore;
using NodaTime;
using NoteAppBackend.DomainModels.DataTransferObjects;

namespace NoteAppBackend.Persistence.PersistenceServices;

public static class NotesQueryService
{
    public static readonly Func<NoteAppBackendContext, List<NoteSummaryDto>> GetAllNotes
        = EF.CompileQuery(
            static (NoteAppBackendContext context) =>
            context.Notes.AsNoTracking()
                .Include(n => n.Type)
                .OrderByDescending(n => n.CreatedAt)
                .Select(static n => n.MapNoteToNoteSummaryDto())
                .ToList()
        );

    public static readonly Func<NoteAppBackendContext, Instant, IEnumerable<NotePagedSummary>> GetPagedNotes
        = EF.CompileQuery(
            static (NoteAppBackendContext context, Instant cursor) =>
            context.Notes.AsNoTracking()
                .Include(n => n.Type)
                .Where(n => n.CreatedAt > cursor)
                .OrderByDescending (n => n.CreatedAt)
                .Select(static n => n.MaptNoteToNotePagedSummaryDto()).Take(10)
            );

    public static readonly Func<NoteAppBackendContext, Guid, NoteDto?> GetNoteById
        = EF.CompileQuery(
            static (NoteAppBackendContext context, Guid noteId) =>
            context.Notes.AsNoTracking()
                .Include(n => n.Type)
                .Where(n => n.Id == noteId)
                .Select(static n => n.MapNoteToNoteDto())
                .SingleOrDefault()
            );
}
