using FCG.Application.Profile;
using FCG.Domain.Authentication;
using FCG.Domain.Common.Response;
using FCG.Domain.Profile;
using FCG.Infrastructure.CorrelationId;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;
using Moq;
using Reqnroll;
using Shouldly;

namespace FCG.Application.Tests.User;

[Binding]
public class CreateUserStepDefinitions
{
    private static readonly Mock<IUserRepository> _userRepository = new();
    private static readonly Mock<ILoginRepository> _loginRepository = new();

    private static readonly Mock<Serilog.ILogger> _serilogLogger = new();
    private static readonly Mock<ICorrelationIdGenerator> _correlationIdGenerator = new();
    private static readonly Mock<BaseLogger> _logger = new(_serilogLogger.Object, _correlationIdGenerator.Object);

    private UserService? _userService;
    private UserDto? _userDto;

    private string _email = string.Empty;
    private ResponseDto<UserDto> _userResponseDto = null!;

    private readonly Guid _existedUser = Guid.NewGuid();

    [Given("the user started the registration")]
    public void GivenTheUserStartedTheRegistration()
    {
        _loginRepository.Setup(_loginRepository => _loginRepository.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LoginModel?)null);

        _userRepository.Setup(x => x.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserModel { Id = _existedUser, FirstName = string.Empty, LastName = string.Empty, Role = Role.Guest });
        _loginRepository.Setup(x => x.AddAsync(It.IsAny<LoginModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LoginModel { Id = Guid.NewGuid(), Email = _email, Password = string.Empty });

        _userService = new UserService(_userRepository.Object, _loginRepository.Object, _logger.Object);
    }

    [Given("add the values to the user")]
    public void GivenAddTheValuesToTheUser(DataTable dataTable)
    {
        var items = dataTable.CreateSet<(string Email, string Password, string FirstName, string LastName, Role Role)>();
        foreach (var item in items)
        {
            _userDto = new()
            {
                Email = item.Email,
                Password = item.Password,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Role = item.Role
            };
            _email = item.Email;
        }
    }

    [When("the client sends the request")]
    public async Task WhenTheClientSendsTheRequest() =>
        _userResponseDto = await _userService!.CreateUserAsync(_userDto!, CancellationToken.None);

    [Then("the user is created and the id is returned")]
    public void ThenTheUserIsCreatedAndTheIdIsReturned() =>
        _userResponseDto.Data!.Id.ShouldBe(_existedUser);

    [Given("the user already exists in the system")]
    public void GivenTheUserAlreadyExistsInTheSystem()=>
        _loginRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LoginModel { Email = _email, Password = string.Empty });

    [Then("the user is not created with the message {string}")]
    public void ThenTheUserIsNotCreatedWithTheMessage(string messageError) =>
        messageError.ShouldBe(_userResponseDto.Message);

}
