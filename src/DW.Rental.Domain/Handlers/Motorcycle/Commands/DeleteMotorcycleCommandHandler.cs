using DW.Rental.Domain.Exception;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Motorcycle.Commands;

public sealed class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleRequest, Result<DeleteMotorcycleResponse>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public DeleteMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<Result<DeleteMotorcycleResponse>> Handle(DeleteMotorcycleRequest createMotoRequest, CancellationToken cancellationToken)
    {
        var motorcycleDomain = await _motorcycleRepository.GetByLicensePlateAsync(createMotoRequest.LicensePlate, cancellationToken);

        if (motorcycleDomain is null)
            return Result.Error<DeleteMotorcycleResponse>(new NotFoundException("Motorcycle not found"));


        await _motorcycleRepository.DeleteAsync(motorcycleDomain!.LicensePlate, cancellationToken);

        return Result.Success(new DeleteMotorcycleResponse());
    }
}
