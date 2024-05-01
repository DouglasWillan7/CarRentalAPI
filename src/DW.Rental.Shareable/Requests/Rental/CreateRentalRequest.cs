using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Responses.Rental;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Rental;

public record class CreateRentalRequest(DateTime DataInicial, DateTime DataFinalPrevista, PlanEnum PlanEnum, Guid DeliverymanId, CnhTypeEnum DeliverymanCategory) : IRequest<Result<CreateRentalResponse>>;