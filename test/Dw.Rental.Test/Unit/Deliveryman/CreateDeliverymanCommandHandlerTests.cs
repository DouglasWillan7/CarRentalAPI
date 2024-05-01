using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Handlers.Deliveryman.Commands;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Deliveryman;
using NSubstitute;

namespace Dw.Rental.Test.Unit.Deliveryman;

public class CreateDeliverymanCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeliverymanAlreadyExists_ReturnsSuccess()
    {
        // Arrange
        var deliverymanRepository = Substitute.For<IDeliverymanRepository>();
        var handler = new CreateDeliverymanCommandHandler(deliverymanRepository);

        var request = new CreateDeliverymanRequest(Name: "Name", Password: "123456", Cnpj: "18794949", CnhType: CnhTypeEnum.A, CnhNumber: 8498198, Birthday: new DateOnly(1997, 01, 09));
        var existingDeliveryman = new DeliverymanDomain { CNH = request.CnhNumber, Name = request.Name };

        deliverymanRepository.GetByCnhAsync(request.CnhNumber, Arg.Any<CancellationToken>()).Returns(existingDeliveryman);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(existingDeliveryman.CNH, result.Value.CnhNumber);
        Assert.Equal(existingDeliveryman.Name, result.Value.Name);
        Assert.Equal(existingDeliveryman.CnhType, result.Value.CnhType);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var deliverymanRepository = Substitute.For<IDeliverymanRepository>();
        var handler = new CreateDeliverymanCommandHandler(deliverymanRepository);
        var request = new CreateDeliverymanRequest(Name: "Name", Password: "123456", Cnpj: "18794949", CnhType: CnhTypeEnum.A, CnhNumber: 8498198, Birthday: new DateOnly(1997, 01, 09));
        var createdDeliveryman = new DeliverymanDomain { CNH = request.CnhNumber, Name = request.Name };


        deliverymanRepository.GetByCnhAsync(request.CnhNumber, Arg.Any<CancellationToken>()).Returns((DeliverymanDomain)null);
        deliverymanRepository.CreateAsync(Arg.Any<DeliverymanDomain>(), Arg.Any<CancellationToken>()).Returns(createdDeliveryman);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(createdDeliveryman.CNH, result.Value.CnhNumber);
        Assert.Equal(createdDeliveryman.Name, result.Value.Name);
    }
}
