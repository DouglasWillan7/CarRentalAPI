using DW.Rental.Domain.Domains;

namespace DW.Rental.Domain.Repositories;

public interface IMotorcycleRepository
{
    Task<MotorcycleDomain?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
    Task<MotorcycleDomain?> GetAvailableAsync(CancellationToken cancellationToken);
    Task<MotorcycleDomain> CreateAsync(MotorcycleDomain motorcycleDomain, CancellationToken cancellationToken);
    Task<MotorcycleDomain> UpdateLicensePlateAsync(string oldLicensePlate, string newLicensePlate, CancellationToken cancellationToken);
    Task DeleteAsync(string placa, CancellationToken cancellationToken);
    Task<MotorcycleDomain> UpdateAvailable(Guid motorcycleId);
}
