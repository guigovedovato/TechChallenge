using Bogus;
using FCG.Application.Game;
using FCG.Domain.Common.Response;
using FCG.Domain.Game;
using FCG.Infrastructure.CorrelationId;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;
using Moq;
using Shouldly;

namespace FCG.Application.Tests.Game;

public class GameServiceTests
{
    private static readonly Mock<IGameRepository> _mockGameRepository = new();
    private static readonly Mock<BaseLogger> _mockLogger = new(Mock.Of<Serilog.ILogger>(), Mock.Of<ICorrelationIdGenerator>());

    private const string ERROR_MESSAGE = "Game with name {0} already exists";
    private readonly Guid EXISTED_GAME = Guid.NewGuid();

    private readonly GameService gameService;

    public GameServiceTests() => gameService = new(_mockGameRepository.Object, _mockLogger.Object);

    [Fact]
    public async Task CreateGameAsync_GameIsCreated_GameReturned()
    {
        // Arrange
        var expected = new Faker<GameDto>()
            .RuleFor(g => g.Id, _ => EXISTED_GAME)
            .RuleFor(g => g.IsActive, _ => true)
            .Generate(1)
            .First();

        _mockGameRepository.Setup(x => x.AddAsync(It.IsAny<GameModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GameModel
            {
                Id = expected.Id,
                Description = expected.Description,
                Name = expected.Name,
                Publisher = expected.Publisher
            });

        // Act
        ResponseDto<GameDto> actual = await gameService.CreateGameAsync(expected, CancellationToken.None);

        // Assert
        actual.Data!.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateGameAsync_GameIsNotCreated_ErrorMessageReturned()
    {
        // Arrange
        var expected = new Faker<GameDto>()
            .RuleFor(g => g.Id, _ => EXISTED_GAME)
            .RuleFor(g => g.Name, _ => "Existing Game Name")
            .Generate(1)
            .First();

        _mockGameRepository.Setup(x => x.GetGameByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GameModel
            {
                Id = expected.Id,
                Description = expected.Description,
                Name = expected.Name,
                Publisher = expected.Publisher
            });

        // Act
        ResponseDto<GameDto> actual = await gameService.CreateGameAsync(expected, CancellationToken.None);

        // Assert
        actual.Message.ShouldBe(string.Format(ERROR_MESSAGE, expected.Name));
    }
}
