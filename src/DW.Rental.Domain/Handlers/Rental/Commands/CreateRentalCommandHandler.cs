using DW.Rental.Domain.Exception;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Messages;
using DW.Rental.Shareable.Requests.Rental;
using DW.Rental.Shareable.Responses.Rental;
using MassTransit;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Rental.Commands;

public class CreateRentalCommandHandler : IRequestHandler<CreateRentalRequest, Result<CreateRentalResponse>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IBus _bus;


    public CreateRentalCommandHandler(IMotorcycleRepository motorcycleRepository, IBus bus)
    {
        _motorcycleRepository = motorcycleRepository;
        _bus = bus;
    }

    public async Task<Result<CreateRentalResponse>> Handle(CreateRentalRequest createRentalRequest, CancellationToken cancellationToken)
    {
        var motorcycleDomain = await _motorcycleRepository.GetAvailableAsync(cancellationToken);

        if (motorcycleDomain is null)
            return Result.Error<CreateRentalResponse>(new NotFoundException("Dont have avaliable motorcycles"));

        if (createRentalRequest.DeliverymanCategory == CnhTypeEnum.B)
            return Result.Error<CreateRentalResponse>(new InvalidCategoryException("Deliveryman category invalid"));

        var rentalId = Guid.NewGuid();
        await _bus.Publish(new RentalMessage(
            rentalId,
            DateOnly.FromDateTime(createRentalRequest.DataInicial),
            DateOnly.FromDateTime(createRentalRequest.DataFinalPrevista),
            createRentalRequest.PlanEnum,
            createRentalRequest.DeliverymanId,
            motorcycleDomain.Id,
            CalculateTotalValue(createRentalRequest.PlanEnum, createRentalRequest.DataInicial, createRentalRequest.DataFinalPrevista)), cancellationToken);

        return Result.Success(new CreateRentalResponse(rentalId, "Rental Created"));
    }

    private decimal CalculateTotalValue(PlanEnum planEnum, DateTime dataInicial, DateTime dataFinalPrevista)
    {
        var planValue = Plan.PlanList().GetValueOrDefault(planEnum);

        var quantidadeDias = Math.Round(dataFinalPrevista.Subtract(dataInicial).TotalDays);

        switch (planEnum)
        {
            case PlanEnum.semanal:
                if (quantidadeDias == 7)
                    return (decimal)(planValue * quantidadeDias);

                if (quantidadeDias < 7)
                    return (decimal)(planValue * quantidadeDias + ((7 - quantidadeDias) * planValue * 0.2));


                return (decimal)(((quantidadeDias - 7) * 50) + (planValue * quantidadeDias));
            case PlanEnum.quizenal:
                if (quantidadeDias == 15)
                    return (decimal)(planValue * quantidadeDias);

                if (quantidadeDias < 15)
                    return (decimal)(planValue * quantidadeDias + ((15 - quantidadeDias) * planValue * 0.4));

                return (decimal)(((quantidadeDias - 7) * 50) + (planValue * quantidadeDias));
            case PlanEnum.mensal:
                if (quantidadeDias == 30)
                    return (decimal)(planValue * quantidadeDias);

                return (decimal)(((quantidadeDias - 30) * 50) + (planValue * quantidadeDias));
            case PlanEnum.quarentaecincodias:
                if (quantidadeDias == 45)
                    return (decimal)(planValue * quantidadeDias);

                return (decimal)(((quantidadeDias - 45) * 50) + (planValue * quantidadeDias));
            case PlanEnum.cinquentadias:
                if (quantidadeDias == 50)
                    return (decimal)(planValue * quantidadeDias);

                return (decimal)(((quantidadeDias - 50) * 50) + (planValue * quantidadeDias));
            default:
                return 0;
        }
    }
}
