using FCG.Application.Store;
using FCG.Domain.Common.Response;
using FCG.Domain.Store;
using FCG.Infrastructure.CorrelationId;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;
using Moq;
using Reqnroll;
using Shouldly;

namespace FCG.Application.Tests.Game;

[Binding]
public class CreateGameStepDefinitions
{
    private static readonly Mock<IGameRepository> _gameRepository = new();

    private static readonly Mock<Serilog.ILogger> _serilogLogger = new();
    private static readonly Mock<ICorrelationIdGenerator> _correlationIdGenerator = new();
    private static readonly Mock<BaseLogger> _logger = new(_serilogLogger.Object, _correlationIdGenerator.Object);

    private GameService? _gameService;
    private GameDto? _gameDto;

    private string _name = string.Empty;
    private ResponseDto<GameDto> _gameResponseDto = null!;

    private readonly Guid _existedGame = Guid.NewGuid();

    [Given("the user started the creation of a new game")]
    public void GivenTheUserStartedTheCreationOfANewGame()
    {
        _gameRepository.Setup(x => x.AddAsync(It.IsAny<GameModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GameModel { Id = _existedGame, Description = string.Empty, Name = string.Empty, Publisher = string.Empty });

        _gameService = new GameService(_gameRepository.Object, _logger.Object);
    }

    [Given("add the values to the game")]
    public void GivenAddTheValuesToTheGame(DataTable dataTable)
    {
        IEnumerable<(string Name, string Description, string ImageUrl, Genre Genre, string Publisher, DateTime ReleaseDate, decimal Price)> items = dataTable.CreateSet<(string Name, string Description, string ImageUrl, Genre Genre, string Publisher, DateTime ReleaseDate, decimal Price)>();
        foreach ((string Name, string Description, string ImageUrl, Genre Genre, string Publisher, DateTime ReleaseDate, decimal Price) item in items)
        {
            _gameDto = new()
            {
                Name = item.Name,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                Genre = item.Genre,
                Publisher = item.Publisher,
                ReleaseDate = item.ReleaseDate,
                Price = item.Price
            };
            _name = item.Name;
        }
    }

    [When("the user sends the request")]
    public async Task WhenTheUserSendsTheRequest() =>
        _gameResponseDto = await _gameService!.CreateGameAsync(_gameDto!, CancellationToken.None);

    [Then("the game is created and the id is returned")]
    public void ThenTheGameIsCreatedAndTheIdIsReturned() =>
        _gameResponseDto.Data!.Id.ShouldBe(_existedGame);

    [Given("the game already exists in the system")]
    public void GivenTheGameAlreadyExistsInTheSystem() =>
        _gameRepository.Setup(x => x.GetGameByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GameModel { Name = _name, Description = string.Empty, Publisher = string.Empty });

    [Then("the game is not created with the message {string}")]
    public void ThenTheGameIsNotCreatedWithTheMessage(string messageError) =>
        messageError.ShouldBe(_gameResponseDto.Message);
}
