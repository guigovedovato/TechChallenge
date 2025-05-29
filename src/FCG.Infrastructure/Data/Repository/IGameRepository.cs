using FCG.Domain.Store;

namespace FCG.Infrastructure.Data.Repository;

public interface IGameRepository : IRepository<GameModel>
{
    Task<IEnumerable<GameModel>> GetGameByFilterAsync(Filter filter, CancellationToken cancellationToken);

    Task<GameModel?> GetGameByNameAsync(string name, CancellationToken cancellationToken);
}
