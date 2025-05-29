using FCG.Domain.Common.Response;
using FCG.Domain.Profile;
using FCG.Domain.Store;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;

namespace FCG.Application.Store;

public class GameService(IGameRepository _gameRepository, BaseLogger _logger) : IGameService
{
    public async Task<ResponseDto<GameDto>> CreateGameAsync(GameDto gameDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creating game: {gameDto.Name}");

        GameModel? gameModel = await _gameRepository.GetGameByNameAsync(gameDto.Name, cancellationToken);
        if (gameModel is not null)
        {
            _logger.LogError($"Game with name {gameDto.Name} already exists");
            return new(null, $"Game with name {gameDto.Name} already exists");
        }

        GameModel response = await _gameRepository.AddAsync(gameDto.ToGameModel(), cancellationToken);
        return new(response.ToGameDto(), string.Empty);
    }

    public async Task<bool> DeleteGameAsync(Guid gameId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting game with ID: {gameId}");
        return await _gameRepository.DeleteAsync(gameId, cancellationToken);
    }

    public async Task<IEnumerable<GameDto>> GetAllGamesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all games");
        IEnumerable<GameModel> games = await _gameRepository.GetAllAsync(cancellationToken);
        return games.Select(game => game.ToGameDto());
    }

    public async Task<IEnumerable<GameDto>> GetGameByFilterAsync(Filter filter, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching games with filter: {filter}");
        IEnumerable<GameModel> games = await _gameRepository.GetGameByFilterAsync(filter, cancellationToken);
        return games.Select(game => game.ToGameDto());
    }

    public async Task<GameDto> GetGameByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching game with ID: {gameId}");
        GameModel game = await _gameRepository.GetByIdAsync(gameId, cancellationToken);
        return game.ToGameDto();
    }

    public async Task<ResponseDto<GameDto>> UpdateGameAsync(Guid gameId, GameDto gameDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating game with ID: {gameId}");
        GameModel gameModel = gameDto.ToGameModel();
        gameModel.Id = gameId;
        GameModel response = await _gameRepository.UpdateAsync(gameId, gameModel, cancellationToken);
        return new(response.ToGameDto(), string.Empty);
    }
}
