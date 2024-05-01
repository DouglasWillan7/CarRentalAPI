using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Motorcycle;

public record CreateMotorcycleRequest(int Year, string Model, string LicensePlate) : IRequest<Result<CreateMotorcycleResponse>>;
