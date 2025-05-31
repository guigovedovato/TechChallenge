using Bogus;
using FCG.Application.Login;
using FCG.Domain.Login;
using FCG.Infrastructure.Authentication;
using FCG.Infrastructure.CorrelationId;
using FCG.Infrastructure.Data.Repository;
using FCG.Infrastructure.Log;
using Moq;
using Shouldly;

namespace FCG.Application.Tests.Login;

public class LoginServiceTests
{
    private static readonly Mock<ILoginRepository> _mockLoginRepository = new();
    private static readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator = new();
    private static readonly Mock<BaseLogger> _mockLogger = new(Mock.Of<Serilog.ILogger>(), Mock.Of<ICorrelationIdGenerator>());

    private readonly Guid EXISTED_LOGIN = Guid.NewGuid();
    private const string EMAIL = "test@company.com";
    private const string PASSWORD = "StrongPassword123!";
    private const string HASHED_PASSWORD = "AQAAAAIAAYagAAAAEFvxevg2Oz/DiArPRXiGGbDy29yxw2ZrroJzNh68WHeARuCaupa6cqkHRj2x0B2S8A==";

    private readonly LoginService loginService;

    public LoginServiceTests() => loginService = new(_mockJwtTokenGenerator.Object, _mockLoginRepository.Object, _mockLogger.Object);

    [Fact]
    public async Task LoginAsync_UserLogged_TokenReturned()
    {
        // Arrange
        var expected = new Faker<LoginDto>()
            .RuleFor(u => u.Id, _ => EXISTED_LOGIN)
            .RuleFor(u => u.Email, _ => EMAIL)
            .RuleFor(u => u.Password, _ => "AdminPassword!123")
            .RuleFor(u => u.Token, _ => "GeneratedToken")
            .Generate(1)
            .First();

        _mockLoginRepository.Setup(_loginRepository => _loginRepository.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LoginModel
            {
                Email = expected.Email,
                Password = HASHED_PASSWORD,
                User = new Domain.User.UserModel
                {
                    Role = Domain.User.Role.Admin,
                    FirstName = "Admin",
                    LastName = "User",
                }
            });

        _mockJwtTokenGenerator.Setup(_jwtTokenGenerator => _jwtTokenGenerator.GenerateTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync("GeneratedToken");

        // Act
        LoginDto actual = await loginService.LoginAsync(expected, CancellationToken.None);

        // Assert
        actual.Email.ShouldBe(expected.Email);
        actual.Token.ShouldBe(expected.Token);
    }

    [Fact]
    public async Task LoginAsync_UserNotFound_WarningMessageLogged()
    {
        // Arrange
        var expected = new Faker<LoginDto>()
            .RuleFor(u => u.Email, _ => EMAIL)
            .Generate(1)
            .First();

        _mockLoginRepository.Setup(_loginRepository => _loginRepository.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LoginModel?)null);

        // Act
        LoginDto actual = await loginService.LoginAsync(expected, CancellationToken.None);

        // Assert
        actual.ShouldBeNull();
        _mockLogger.Verify(logger => logger.LogWarning(It.Is<string>(msg => msg.Contains(string.Format("User not found: {0}", EMAIL)))), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_WarningMessageLogged()
    {
        // Arrange
        var expected = new Faker<LoginDto>()
            .RuleFor(u => u.Id, _ => EXISTED_LOGIN)
            .RuleFor(u => u.Email, _ => EMAIL)
            .RuleFor(u => u.Password, _ => PASSWORD)
            .Generate(1)
            .First();

        _mockLoginRepository.Setup(_loginRepository => _loginRepository.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LoginModel
            {
                Email = expected.Email,
                Password = HASHED_PASSWORD
            });

        // Act
        LoginDto actual = await loginService.LoginAsync(expected, CancellationToken.None);

        // Assert
        actual.ShouldBeNull();
        _mockLogger.Verify(logger => logger.LogWarning(It.Is<string>(msg => msg.Contains(string.Format("Invalid password for user: {0}", EMAIL)))), Times.Once);
    }
}
