using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Repositories;
using DW.Rental.Shareable.Messages;
using MassTransit;

namespace Dw.Rental.Consumer;

public class CreateRentalConsumer : IConsumer<RentalMessage>
{
    private readonly ILogger<CreateRentalConsumer> _logger;
    private readonly IRentalRepository _rentalRepository;
    private readonly IMotorcycleRepository _motorcycleRepository;
    public CreateRentalConsumer(ILogger<CreateRentalConsumer> logger, IRentalRepository rentalRepository, IMotorcycleRepository motorcycleRepository)
    {
        _logger = logger;
        _rentalRepository = rentalRepository;
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task Consume(ConsumeContext<RentalMessage> context)
    {
        await _rentalRepository.CreateAsync((RentalDomain)context.Message);
        await _motorcycleRepository.UpdateAvailable(context.Message.MotorcycleId);
    }
}
