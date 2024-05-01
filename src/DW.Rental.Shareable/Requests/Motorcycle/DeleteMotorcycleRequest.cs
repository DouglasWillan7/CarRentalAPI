using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Motorcycle;

public record DeleteMotorcycleRequest(string LicensePlate) : IRequest<Result<DeleteMotorcycleResponse>>;
