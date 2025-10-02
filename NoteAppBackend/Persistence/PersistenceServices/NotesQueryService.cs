using Microsoft.EntityFrameworkCore;
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
                .Select(static n => new NoteSummaryDto(
                    n.Id, n.NoteTitle, n.NoteDate.ToString(), new NoteTypeSummary(n.Type.Id, n.Type.Name,
                        n.Type.ColorCode))).ToList());
}
