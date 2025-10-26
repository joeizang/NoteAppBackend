using System.IO.Compression;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using NoteAppBackend.DomainModels;
using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.Persistence.PersistenceServices;

public interface ICommandService
{
    Task<Result<Note>> Create(Note entity);
    Task<Result<NoteType>> CreateNoteType(NoteType entity);

    Task<Result<Note>> Update(NoteUpdateDto entity);
    Task<Result<NoteType>> UpdateNoteType(NoteType entity);

    Task DeleteNote(Guid id);
    Task DeleteNoteType(Guid id);
}

public class CommandService : ICommandService
{
    private readonly NoteAppBackendContext _db;
    public CommandService(NoteAppBackendContext context)
    {
        _db = context;
    }
    public async Task<Result<Note>> Create(Note entity)
    {
        try
        {
            _db.Notes.Add(entity);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Result<Note>(entity);
        }
        catch (Exception ex)
        {
            return new Result<Note>(ex);
        }
    }
    public async Task<Result<NoteType>> CreateNoteType(NoteType entity)
    {
        try
        {
            _db.NoteTypes.Add(entity);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Result<NoteType>(entity);
        }
        catch (Exception ex)
        {
            return new Result<NoteType>(ex);
        }
    }

    public async Task DeleteNote(Guid id)
    {
        try
        {
            var result = await _db.Notes.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(s => s.IsDeleted, true))
                .ConfigureAwait(false);
            // return new Result<Note>();
        }
        catch (Exception ex)
        {
            // return new Result<Note>(ex);
            throw;
        }
    }

    public async Task DeleteNoteType(Guid id)
    {
        try
        {
            var result = await _db.NoteTypes.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(s => s.IsDeleted, true))
                .ConfigureAwait(false);
            //return new Result<NoteType>();
        }
        catch (Exception ex)
        {
            throw;
            // return new Result<NoteType>(ex);
        }
    }

    public async Task<Result<Note>> Update(NoteUpdateDto entity)
    {
        try
        {
            // entity.UpdatedAt = TimeOnly.FromDateTime(DateTime.UtcNow);
            // _db.Entry(entity).State = EntityState.Modified;
            // await _db.SaveChangesAsync().ConfigureAwait(false);
            var newMediaList = 
            await _db.Notes.Where(x => x.Id == entity.NoteId)
                .ExecuteUpdateAsync(note => note
                    .SetProperty(n => n.NoteTitle, entity.Title)
                    .SetProperty(n => n.NoteBody, entity.NoteBody)
                    .SetProperty(n => n.Media, entity.Media)
                    .SetProperty(n => n.UpdatedAt, TimeOnly.FromDateTime(DateTime.UtcNow))
                    .SetProperty(n => n.UpdatedOn, DateOnly.FromDateTime(DateTime.UtcNow))
                );
            return new Result<Note>();
        }
        catch (Exception ex)
        {
            return new Result<Note>(ex);
        }
    }
    public async Task<Result<NoteType>> UpdateNoteType(NoteType entity)
    {
        try
        {
            entity.UpdatedAt = TimeOnly.FromDateTime(DateTime.UtcNow);
            entity.UpdatedOn = DateOnly.FromDateTime(DateTime.UtcNow);
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Result<NoteType>(entity);
        }
        catch (Exception ex)
        {
            return new Result<NoteType>(ex);
        }
    }
}
