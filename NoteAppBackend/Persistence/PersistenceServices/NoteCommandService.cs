using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.Persistence.PersistenceServices;

public interface ICommandService<T> where T : BaseEntity
{
    Task<Result<T>> Create(T entity);

    Task<Result<T>> Update(T entity);

    Task<Result<T>> Delete(T entity);
}

public class CommandService<T> : ICommandService<T> where T: BaseEntity
{
    private readonly NoteAppBackendContext _db;
    private DbSet<T> _set;
    public CommandService(NoteAppBackendContext context)
    {
        _db = context;
        _set = _db.Set<T>();
    }
    public async Task<Result<T>> Create(T entity)
    {
        try
        {
            _set.Add(entity);
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Result<T>(entity);
        }
        catch (Exception ex)
        {
            return new Result<T>(ex);
        }
    }

    public async Task<Result<T>> Delete(T entity)
    {
        try
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Result<T>(entity);
        }
        catch (Exception ex)
        {
            return new Result<T>(ex);
        }
    }

    public async Task<Result<T>> Update(T entity)
    {
        try
        {
            entity.UpdatedAt = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Result<T>(entity);
        }
        catch (Exception ex)
        {
            return new Result<T>(ex);
        }
    }
}
