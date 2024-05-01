using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Responses.Motorcycle;
using MediatR;
using OperationResult;

namespace DW.Rental.Domain.Handlers.Motorcycle.Commands;

public sealed class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleRequest, Result<CreateMotorcycleResponse>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public CreateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<Result<CreateMotorcycleResponse>> Handle(CreateMotorcycleRequest createMotoRequest, CancellationToken cancellationToken)
    {
        var motorcycleDomain = await _motorcycleRepository.GetByLicensePlateAsync(createMotoRequest.LicensePlate, cancellationToken);

        if (motorcycleDomain is not null)
            return Result.Success((CreateMotorcycleResponse)motorcycleDomain);

        var createdMotorcycle = await _motorcycleRepository.CreateAsync((MotorcycleDomain)createMotoRequest!, cancellationToken);

        return Result.Success((CreateMotorcycleResponse)createdMotorcycle);
    }
}
