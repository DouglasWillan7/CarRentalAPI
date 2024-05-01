using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Responses.Deliveryman;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Deliveryman;

public record CreateDeliverymanRequest(string Name, string Password, string Cnpj, DateOnly Birthday, int CnhNumber, CnhTypeEnum CnhType) : IRequest<Result<CreateDeliverymanResponse>>;
