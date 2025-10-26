using Microsoft.EntityFrameworkCore;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Helpers;

namespace NoteAppBackend.Persistence.PersistenceServices;

public static class NotesQueryService
{
    public static readonly Func<NoteAppBackendContext, dynamic> GetAllNotes
        = EF.CompileQuery((NoteAppBackendContext context) => context.Notes.AsNoTracking()
                  .Select(n => new NoteSummaryDto(n.Id, n.NoteTitle, n.CreatedAt.ToLongDateString(),
                  n.NoteTypeId, NoteAppHelper.Encode(n.CreatedAt))));

    public static readonly Func<NoteAppBackendContext, DateTime, IEnumerable<NoteSummaryDto>> GetPagedNotes
        = EF.CompileQuery(
            static (NoteAppBackendContext context, DateTime cursor) =>
            context.Notes.AsNoTracking()
                .Where(n => n.CreatedAt > cursor)
                .OrderByDescending(n => n.Id)
                .Select(static n => n.MaptNoteToNotePagedSummaryDto()).Take(2)
            );

    public static readonly Func<NoteAppBackendContext, Guid, NoteDto?> GetNoteById
        = EF.CompileQuery(
            static (NoteAppBackendContext context, Guid noteId) =>
            context.Notes.AsNoTracking()
                .Where(n => n.Id == noteId)
                .Select(static n => n.MapNoteToNoteDto())
                .SingleOrDefault()
            );


    public static readonly Func<NoteAppBackendContext, string, IEnumerable<object?>> 
    GetNoteBySearchParameter = EF.CompileQuery(
        (NoteAppBackendContext context, string searchParam) =>
            context.Notes.AsNoTracking()
                .Where(n => EF.Functions.Like(n.NoteBody, "%" + searchParam + "%") ||
                    EF.Functions.Like(n.NoteTitle, "%" + searchParam + "%"))
                .Select(static n => new { NoteId = n.Id, Title = n.NoteTitle, NoteContent = n.NoteBody })
                .Take(2)
    );

    public static readonly Func<NoteAppBackendContext, NoteTypeSummaryDto[]> GetNoteTypes
        = EF.CompileQuery(
            (NoteAppBackendContext context) =>
            context.NoteTypes.AsNoTracking()
            .OrderByDescending(t => t.Id)
            .Select(t => new NoteTypeSummaryDto(t.Id, t.Name, t.Description, t.ColorCode,
                NoteAppHelper.Encode(t.CreatedAt)))
            .ToArray());

    public static readonly Func<NoteAppBackendContext, Guid, NoteTypeSummaryDto?> GetNoteTypeById
        = EF.CompileQuery(
            static (NoteAppBackendContext context, Guid id) =>
            context.NoteTypes.AsNoTracking()
            .Where(t => t.Id.Equals(id))
            .Select(static t => new NoteTypeSummaryDto(t.Id, t.Name, t.Description, t.ColorCode,
                NoteAppHelper.Encode(t.CreatedAt)))
            .SingleOrDefault()
        );
    
    public static readonly Func<NoteAppBackendContext, NoteTypeSummaryDto[]> GetNoteAllTypes
        = EF.CompileQuery(
            (NoteAppBackendContext context) =>
            context.NoteTypes.AsNoTracking()
             .OrderByDescending(t => t.CreatedAt)
            .Select(t => new NoteTypeSummaryDto(t.Id, t.Name, t.Description, t.ColorCode,
                NoteAppHelper.Encode(t.CreatedAt))).ToArray()
        );
}
