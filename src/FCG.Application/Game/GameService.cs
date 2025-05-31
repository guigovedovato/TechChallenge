using FCG.Domain.Common.Response;
using FCG.Domain.Game;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;

namespace FCG.Application.Game;

public class GameService(IGameRepository _gameRepository, BaseLogger _logger) : IGameService
{
    public async Task<ResponseDto<GameDto>> CreateGameAsync(GameDto gameDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creating game: {gameDto.Name}");

        var gameModel = await _gameRepository.GetGameByNameAsync(gameDto.Name, cancellationToken);
        if (gameModel is not null)
        {
            _logger.LogError($"Game with name {gameDto.Name} already exists");
            return new(null, $"Game with name {gameDto.Name} already exists");
        }

        var response = await _gameRepository.AddAsync(gameDto.ToGameModel(), cancellationToken);
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
        var games = await _gameRepository.GetAllAsync(cancellationToken);
        return games.Select(game => game.ToGameDto());
    }

    public async Task<IEnumerable<GameDto>> GetGameByFilterAsync(Filter filter, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching games with filter: {filter}");
        var games = await _gameRepository.GetGameByFilterAsync(filter, cancellationToken);
        return games.Select(game => game.ToGameDto());
    }

    public async Task<GameDto> GetGameByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching game with ID: {gameId}");
        var game = await _gameRepository.GetByIdAsync(gameId, cancellationToken);
        return game.ToGameDto();
    }

    public async Task<ResponseDto<GameDto>> UpdateGameAsync(Guid gameId, GameDto gameDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating game with ID: {gameId}");
        var gameModel = gameDto.ToGameModel();
        gameModel.Id = gameId;
        var response = await _gameRepository.UpdateAsync(gameId, gameModel, cancellationToken);
        return new(response.ToGameDto(), string.Empty);
    }
}
