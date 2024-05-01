using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Rental.Commands;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Rental;
using MassTransit;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Rental;

public class CreateRentalCommandHandlerTests
{
    [Fact]
    public async Task Handle_NoAvailableMotorcycles_ReturnsError()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var bus = Substitute.For<IBus>();
        var handler = new CreateRentalCommandHandler(motorcycleRepository, bus);

        var request = new CreateRentalRequest(DateTime.Now, DateTime.Now.AddDays(7), PlanEnum.semanal, Guid.NewGuid(), CnhTypeEnum.A);

        motorcycleRepository.GetAvailableAsync(Arg.Any<CancellationToken>()).Returns((MotorcycleDomain)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Dont have avaliable motorcycles", result.Exception.Message);
    }

    [Fact]
    public async Task Handle_InvalidDeliverymanCategory_ReturnsError()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var bus = Substitute.For<IBus>();
        var handler = new CreateRentalCommandHandler(motorcycleRepository, bus);

        var request = new CreateRentalRequest(DateTime.Now, DateTime.Now.AddDays(7), PlanEnum.semanal, Guid.NewGuid(), CnhTypeEnum.B);
        var motorcycleAvailable = new MotorcycleDomain(year: 2009, model: "Titan", licensePlate: "XXX-1234");


        motorcycleRepository.GetAvailableAsync(Arg.Any<CancellationToken>()).Returns(motorcycleAvailable);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Deliveryman category invalid", result.Exception.Message);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var motorcycleRepository = Substitute.For<IMotorcycleRepository>();
        var bus = Substitute.For<IBus>();
        var handler = new CreateRentalCommandHandler(motorcycleRepository, bus);

        var request = new CreateRentalRequest(DateTime.Now, DateTime.Now.AddDays(7), PlanEnum.semanal, Guid.NewGuid(), CnhTypeEnum.A);
        var motorcycle = new MotorcycleDomain(year: 2009, model: "Titan", licensePlate: "XXX-1234");

        motorcycleRepository.GetAvailableAsync(Arg.Any<CancellationToken>()).Returns(motorcycle);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Rental Created", result.Value.Message);
    }
}
