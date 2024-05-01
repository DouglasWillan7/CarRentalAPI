using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Deliveryman;
using DW.Rental.Shareable.Responses.Deliveryman;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Deliveryman.Commands;

public sealed class CreateDeliverymanCommandHandler : IRequestHandler<CreateDeliverymanRequest, Result<CreateDeliverymanResponse>>
{
    private readonly IDeliverymanRepository _deliverymanRepository;

    public CreateDeliverymanCommandHandler(IDeliverymanRepository deliverymanRepository)
    {
        _deliverymanRepository = deliverymanRepository;
    }

    public async Task<Result<CreateDeliverymanResponse>> Handle(CreateDeliverymanRequest createDeliverymanRequest, CancellationToken cancellationToken)
    {
        var deliverymanDomain = await _deliverymanRepository.GetByCnhAsync(createDeliverymanRequest.CnhNumber, cancellationToken);

        if (deliverymanDomain is not null)
            return Result.Success((CreateDeliverymanResponse)deliverymanDomain);

        var createdDeliveryman = await _deliverymanRepository.CreateAsync((DeliverymanDomain)createDeliverymanRequest!, cancellationToken);

        return Result.Success((CreateDeliverymanResponse)createdDeliveryman);
    }
}
