using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Motorcycle.Commands;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Motorcycle;

public class CreateMotorcycleCommandHandlerTests
{
    private readonly string LICENSE_PLATE = "XXX-8423";

    [Fact]
    public async Task Handle_LicensePlateAlreadyExistsResilience_ReturnsOk()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new CreateMotorcycleCommandHandler(motorcycleRepository);

        var request = new CreateMotorcycleRequest(2009, "Titan", LICENSE_PLATE);
        var existingMotorcycle = new MotorcycleDomain(year: 2009, model: "Titan", licensePlate: request.LicensePlate);

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns(existingMotorcycle);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.LicensePlate, request.LicensePlate);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new CreateMotorcycleCommandHandler(motorcycleRepository);

        var request = new CreateMotorcycleRequest(2009, "Titan", LICENSE_PLATE);

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns((MotorcycleDomain)null);

        var createdMotorcycle = new MotorcycleDomain(year: 2009, model: "Titan", licensePlate: request.LicensePlate);

        motorcycleRepository.CreateAsync(Arg.Any<MotorcycleDomain>(), Arg.Any<CancellationToken>()).Returns(createdMotorcycle);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(createdMotorcycle.LicensePlate, result.Value.LicensePlate);
        Assert.Equal(createdMotorcycle.Year, result.Value.Year);
        Assert.Equal(createdMotorcycle.Model, result.Value.Model);
    }
}
