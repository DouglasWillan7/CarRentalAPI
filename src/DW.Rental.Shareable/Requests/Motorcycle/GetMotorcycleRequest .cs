using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Motorcycle;

public record GetMotorcycleRequest(string LicensePlate) : IRequest<Result<GetMotorcycleResponse>>;
