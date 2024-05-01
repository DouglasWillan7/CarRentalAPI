using DW.Rental.Domain.Configs;
using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Login.Commands;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Login;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Login;

public class LoginCommandHandlerTests
{
    private readonly string SECRET_KEY = "fedaf7d8863b48e197b9287d492b708e";


    [Fact]
    public async Task Handle_InvalidLogin_ReturnsError()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var deliverymanRepository = Substitute.For<IDeliverymanRepository>();
        var jwtSettings = new JwtSettings { SecretKey = SECRET_KEY };

        var handler = new LoginCommandHandler(userRepository, deliverymanRepository, Options.Create(jwtSettings));

        var loginRequest = new LoginRequest(Username: "invalid_login", Password: "invalid_password");

        userRepository.GetAsync(loginRequest.Username, loginRequest.Password, Arg.Any<CancellationToken>()).Returns((UserDomain)null);

        // Act
        var result = await handler.Handle(loginRequest, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User not found", result.Exception.Message);
    }

    [Fact]
    public async Task Handle_ValidLogin_ReturnsSuccess()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var deliverymanRepository = Substitute.For<IDeliverymanRepository>();
        var jwtSettings = new JwtSettings { SecretKey = SECRET_KEY };

        var user = new UserDomain { Id = Guid.NewGuid(), Username = "username", Role = RoleTypeEnum.Admin };
        userRepository.GetAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(user);
        var handler = new LoginCommandHandler(userRepository, deliverymanRepository, Options.Create(jwtSettings));

        var loginRequest = new LoginRequest(Username: "username", Password: "password");

        // Act
        var result = await handler.Handle(loginRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value.Token);
    }

    [Fact]
    public async Task Handle_DeliverymanLogin_ReturnsSuccessWithDeliverymanData()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        var deliverymanRepository = Substitute.For<IDeliverymanRepository>();
        var jwtSettings = new JwtSettings { SecretKey = SECRET_KEY };

        var user = new UserDomain { Id = Guid.NewGuid(), Username = "username", Role = RoleTypeEnum.Deliveryman };
        var deliveryman = new DeliverymanDomain { Id = Guid.NewGuid(), CnhType = CnhTypeEnum.A };
        userRepository.GetAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(user);
        deliverymanRepository.GetByUserIdAsync(Guid.NewGuid(), Arg.Any<CancellationToken>()).Returns(deliveryman);

        var handler = new LoginCommandHandler(userRepository, deliverymanRepository, Options.Create(jwtSettings));

        var loginRequest = new LoginRequest(Username: "username", Password: "password");

        // Act
        var result = await handler.Handle(loginRequest, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value.Token);
    }
}
