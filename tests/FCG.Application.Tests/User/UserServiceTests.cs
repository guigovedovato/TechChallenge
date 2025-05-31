using Bogus;
using FCG.Application.User;
using FCG.Domain.Login;
using FCG.Domain.Common.Response;
using FCG.Domain.User;
using FCG.Infrastructure.CorrelationId;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;
using Moq;
using Shouldly;

namespace FCG.Application.Tests.User;

public class UserServiceTests
{
    private static readonly Mock<IUserRepository> _mockUserRepository = new();
    private static readonly Mock<ILoginRepository> _mockLoginRepository = new();
    private static readonly Mock<BaseLogger> _mockLogger = new(Mock.Of<Serilog.ILogger>(), Mock.Of<ICorrelationIdGenerator>());

    private readonly Guid EXISTED_USER = Guid.NewGuid();
    private const string EMAIL = "test@company.com";
    private const string PASSWORD = "StrongPassword123!";

    private readonly UserService userService;

    public UserServiceTests() => userService = new(_mockUserRepository.Object, _mockLoginRepository.Object, _mockLogger.Object);

    [Fact]
    public async Task CreateUserAsync_UserIsCreated_UserReturned()
    {
        // Arrange
        var expected = new Faker<UserDto>()
            .RuleFor(u => u.Id, _ => EXISTED_USER)
            .RuleFor(u => u.IsActive, _ => true)
            .RuleFor(u => u.Role, _ => Role.User)
            .RuleFor(u => u.Email, _ => EMAIL)
            .RuleFor(u => u.Password, _ => PASSWORD)
            .Generate(1)
            .First();

        _mockLoginRepository.Setup(_loginRepository => _loginRepository.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LoginModel?)null);

        _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserModel 
            { 
                Id = expected.Id,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                Role = expected.Role
            });
        _mockLoginRepository.Setup(x => x.AddAsync(It.IsAny<LoginModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LoginModel { Id = Guid.NewGuid(), Email = EMAIL, Password = PASSWORD });

        // Act
        ResponseDto<UserDto> actual = await userService.CreateUserAsync(expected, CancellationToken.None);

        // Assert
        expected.Password = string.Empty; // Password should not be returned in the response
        actual.Data!.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("Invalid email format", "", PASSWORD)]
    [InlineData("Invalid password format", EMAIL, "")]
    public async Task CreateUserAsync_UserIsNotCreated_ErrorMessageReturned(
        string errorMessage,
        string email,
        string password)
    {
        // Arrange
        var expected = new Faker<UserDto>()
            .RuleFor(u => u.Id, _ => EXISTED_USER)
            .RuleFor(u => u.Email, _ => email)
            .RuleFor(u => u.Password, _ => password)
            .Generate(1)
            .First();

        _mockLoginRepository.Setup(_loginRepository => _loginRepository.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LoginModel?)null);

        // Act
        ResponseDto<UserDto> actual = await userService.CreateUserAsync(expected, CancellationToken.None);

        // Assert
        actual.Message.ShouldBe(errorMessage);
    }

    [Fact]
    public async Task CreateUserAsync_UserIsCreated_UserAlreadyExists()
    {
        // Arrange
        var expected = new Faker<UserDto>()
            .RuleFor(u => u.Id, _ => EXISTED_USER)
            .RuleFor(u => u.Email, _ => EMAIL)
            .Generate(1)
            .First();

        _mockLoginRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LoginModel { Email = EMAIL, Password = string.Empty });

        // Act
        ResponseDto<UserDto> actual = await userService.CreateUserAsync(expected, CancellationToken.None);

        // Assert
        actual.Message.ShouldBe("User with this email already exists");
    }
}
