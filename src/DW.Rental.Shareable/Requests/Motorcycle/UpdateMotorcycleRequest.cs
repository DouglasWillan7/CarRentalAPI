using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Shareable.Requests.Motorcycle;

public record UpdateMotorcycleRequest(string LicensePlate, string NewLicensePlate) : IRequest<Result<UpdateMotorcycleResponse>>;
