using FCG.Domain.Common;
using FCG.Domain.Common.Exceptions;
using FCG.Infrastructure.Data.Context;
using FCG.Infrastructure.Log;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Data.Repository;

public class Repository<T>(ApplicationDbContext _context, BaseLogger _logger) : IRepository<T> where T : EntityBase
{
    protected readonly BaseLogger _logger = _logger;
    protected readonly ApplicationDbContext _context = _context;
    protected readonly DbSet<T> _dbSet = _context.Set<T>();

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        try
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            _dbSet.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"{typeof(T)} added successfully.");

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding {typeof(T)}: {ex.Message}");
            throw;
        }
    }

    public async Task<T> UpdateAsync(Guid id, T entity, CancellationToken cancellationToken)
    {
        try
        {
            var existingEntity = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
            if (existingEntity is null)
            {
                _logger.LogWarning($"{typeof(T)} with ID {id} not found.");
                throw new EntityDoesNotExistException(id, typeof(T));
            }

            entity.Id = id;
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating {typeof(T)}: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
            if (entity is null)
            {
                _logger.LogWarning($"{typeof(T)} with ID {id} not found.");
                throw new EntityDoesNotExistException(id, typeof(T));
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"{typeof(T)} with ID {id} deleted successfully.");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting {typeof(T)}: {ex.Message}");
            throw;
        }
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
            if (entity is null)
            {
                _logger.LogWarning($"{typeof(T)} with ID {id} not found.");
                throw new EntityDoesNotExistException(id, typeof(T));
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving {typeof(T)} with ID {id}: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving all {typeof(T)}: {ex.Message}");
            throw;
        }
    }
}
