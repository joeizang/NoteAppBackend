using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.Persistence.PersistenceServices;

public interface ICommandService<T> where T : BaseEntity
{
    Task<T> Create(T entity);

    Task<T> Update(T entity);

    Task Delete(T entity);
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
    public async Task<T> Create(T entity)
    {
        _set.Add(entity);
        await _db.SaveChangesAsync().ConfigureAwait(false);
        return entity;
    }

    public async Task Delete(T entity)
    {
        entity.IsDeleted = true;
        entity.UpdatedAt = Instant.FromDateTimeUtc(DateTime.UtcNow);
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync().ConfigureAwait(false);
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
