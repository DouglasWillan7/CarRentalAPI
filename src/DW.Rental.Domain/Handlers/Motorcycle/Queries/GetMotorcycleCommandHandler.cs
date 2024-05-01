using DW.Rental.Domain.Exception;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Responses.Login;
using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Motorcycle.Queries;

public sealed class GetMotorcycleCommandHandler : IRequestHandler<GetMotorcycleRequest, Result<GetMotorcycleResponse>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    public GetMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<Result<GetMotorcycleResponse>> Handle(GetMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate, cancellationToken);

        if (motorcycle is null)
        {
            return Result.Error<GetMotorcycleResponse>(new NotFoundException("Motorcycle not found"));
        }

        return Result.Success((GetMotorcycleResponse)motorcycle);
    }
}
