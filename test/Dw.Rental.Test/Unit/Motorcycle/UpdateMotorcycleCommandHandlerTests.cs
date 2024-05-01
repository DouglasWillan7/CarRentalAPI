using Dw.Rental.Test.DataFaker.Motorcycle;
using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Motorcycle.Commands;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Motorcycle;

public class UpdateMotorcycleCommandHandlerTests
{
    private readonly string NEW_LICENSE_PLATE = "XXX-8423";
    private readonly string OLD_LICENSE_PLATE = "XXX-8424";

    [Fact]
    public async Task Handle_MotorcycleNotFound_ReturnsError()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new UpdateMotorcycleCommandHandler(motorcycleRepository);

        var request = new UpdateMotorcycleRequest(LicensePlate: OLD_LICENSE_PLATE, NewLicensePlate: NEW_LICENSE_PLATE);

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns((MotorcycleDomain)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Motorcycle not found", result.Exception.Message);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new UpdateMotorcycleCommandHandler(motorcycleRepository);

        var request = new UpdateMotorcycleRequest(LicensePlate: OLD_LICENSE_PLATE, NewLicensePlate: NEW_LICENSE_PLATE);
        var motorcycleDomain = new MotorcycleDomain(licensePlate: request.LicensePlate, year: 2009, model: "Titan");
        var updatedMotorcycle = new MotorcycleDomain(licensePlate: request.NewLicensePlate, year: 2009, model: "Titan");

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns(motorcycleDomain);
        motorcycleRepository.UpdateLicensePlateAsync(request.LicensePlate, request.NewLicensePlate, Arg.Any<CancellationToken>()).Returns(updatedMotorcycle);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(updatedMotorcycle.LicensePlate, result.Value.LicensePlate);
    }
}
