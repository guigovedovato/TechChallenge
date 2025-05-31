using FCG.Domain.Common.Response;

namespace FCG.Domain.Game;

public interface IGameService
{
    Task<GameDto> GetGameByIdAsync(Guid gameId, CancellationToken cancellationToken);
    Task<IEnumerable<GameDto>> GetAllGamesAsync(CancellationToken cancellationToken);
    Task<ResponseDto<GameDto>> CreateGameAsync(GameDto gameDto, CancellationToken cancellationToken);
    Task<bool> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken);
    Task<ResponseDto<GameDto>> UpdateGameAsync(Guid gameId, GameDto gameDto, CancellationToken cancellationToken);
    Task<IEnumerable<GameDto>> GetGameByFilterAsync(Filter filter, CancellationToken cancellationToken);
}
