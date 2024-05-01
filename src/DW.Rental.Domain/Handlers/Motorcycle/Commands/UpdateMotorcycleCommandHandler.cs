using DW.Rental.Domain.Exception;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Motorcycle.Commands;

public sealed class UpdateMotorcycleCommandHandler : IRequestHandler<UpdateMotorcycleRequest, Result<UpdateMotorcycleResponse>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public UpdateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<Result<UpdateMotorcycleResponse>> Handle(UpdateMotorcycleRequest updateMotoRequest, CancellationToken cancellationToken)
    {
        var motorcycleDomain = await _motorcycleRepository.GetByLicensePlateAsync(updateMotoRequest.LicensePlate, cancellationToken);

        if (motorcycleDomain is null)
        {
            return Result.Error<UpdateMotorcycleResponse>(new NotFoundException("Motorcycle not found"));
        }

        var newMotorcycleDomain = await _motorcycleRepository.GetByLicensePlateAsync(updateMotoRequest.NewLicensePlate, cancellationToken);

        if (newMotorcycleDomain is not null)
        {
            return Result.Error<UpdateMotorcycleResponse>(new NotFoundException("Motorcycle with this plate already exists"));
        }

        var updatedMotorcycle = await _motorcycleRepository.UpdateLicensePlateAsync(updateMotoRequest.LicensePlate, updateMotoRequest.NewLicensePlate, cancellationToken);

        return Result.Success((UpdateMotorcycleResponse)updatedMotorcycle);
    }
}
