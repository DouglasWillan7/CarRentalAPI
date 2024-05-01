using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Motorcycle.Commands;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Motorcycle;

public class DeleteMotorcycleCommandHandlerTests
{
    private readonly string LICENSE_PLATE = "XXX-8423";

    [Fact]
    public async Task Handle_MotorcycleNotFound_ReturnsError()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new DeleteMotorcycleCommandHandler(motorcycleRepository);

        var request = new DeleteMotorcycleRequest(LICENSE_PLATE);

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
        var handler = new DeleteMotorcycleCommandHandler(motorcycleRepository);

        var request = new DeleteMotorcycleRequest(LICENSE_PLATE);
        var motorcycleDomain = new MotorcycleDomain(licensePlate: request.LicensePlate, year: 2009, model: "Titan");

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns(motorcycleDomain);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
