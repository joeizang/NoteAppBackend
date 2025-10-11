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
                .OrderByDescending(n => n.CreatedAt)
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

    public static readonly Func<NoteAppBackendContext, string, IEnumerable<NoteDto?>> GetNoteBySearchParameter
        = EF.CompileQuery(
            static (NoteAppBackendContext context, string searchParam) =>
            context.Notes.AsNoTracking()
                .Include(n => n.Type)
                .Where(n => EF.Functions.ILike(n.NoteBody, $"%{searchParam}%") ||
                    EF.Functions.ILike(n.NoteTitle, $"%{searchParam}%"))
                .OrderByDescending(n => n.CreatedAt)
                .Select(static n => n.MapNoteToNoteDto())
                .Take(20)
                .ToList()
            );

    public static readonly Func<NoteAppBackendContext, IEnumerable<NoteTypeSummaryDto>> GetNoteTypes
        = EF.CompileQuery(
            static (NoteAppBackendContext context) =>
            context.NoteTypes.AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .Select(static t => new NoteTypeSummaryDto(t.Id, t.Name, t.Description, t.ColorCode))
            );

    public static readonly Func<NoteAppBackendContext, Guid, NoteTypeSummaryDto?> GetNoteTypeById
        = EF.CompileQuery(
            static (NoteAppBackendContext context, Guid id) =>
            context.NoteTypes.AsNoTracking()
            .Where(t => t.Id.Equals(id))
            .Select(static t => new NoteTypeSummaryDto(t.Id, t.Name, t.Description, t.ColorCode))
            .SingleOrDefault()
        );
}
