using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Motorcycle.Queries;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Motorcycle;

public class GetMotorcycleCommandHandlerTests
{
    private readonly string LICENSE_PLATE = "XXX-8423";

    [Fact]
    public async Task Handle_InvalidLicensePlate_ReturnsError()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new GetMotorcycleCommandHandler(motorcycleRepository);

        var request = new GetMotorcycleRequest(LicensePlate: LICENSE_PLATE);

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns((MotorcycleDomain)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Motorcycle not found", result.Exception.Message);
    }

    [Fact]
    public async Task Handle_ValidLicensePlate_ReturnsSuccess()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var handler = new GetMotorcycleCommandHandler(motorcycleRepository);

        var request = new GetMotorcycleRequest(LicensePlate: LICENSE_PLATE);
        var motorcycle = new MotorcycleDomain(id: Guid.NewGuid(), licensePlate: request.LicensePlate, year: 2009, model: "Titan");

        motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, Arg.Any<CancellationToken>()).Returns(motorcycle);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(motorcycle.LicensePlate, result.Value.LicensePlate);
        Assert.Equal(motorcycle.Year, result.Value.Year);
        Assert.Equal(motorcycle.Model, result.Value.Model);
    }
}
