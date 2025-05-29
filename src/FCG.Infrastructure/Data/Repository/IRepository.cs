using FCG.Domain.Common;

namespace FCG.Infrastructure.Data.Repository;

public interface IRepository<T> where T : EntityBase
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(Guid id, T entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
