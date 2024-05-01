using DW.Rental.Domain.Domains;

namespace DW.Rental.Domain.Repositories;

public interface IDeliverymanRepository
{
    Task<DeliverymanDomain> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<DeliverymanDomain> GetByCnhAsync(int cnh, CancellationToken cancellationToken);
    Task<DeliverymanDomain> CreateAsync(DeliverymanDomain motorcycleDomain, CancellationToken cancellationToken);
    Task<DeliverymanDomain> UpdateFotoAsync(Guid deliverymanId, string photoName, CancellationToken cancellationToken);
    Task DeleteAsync(string cpf, CancellationToken cancellationToken);
    Task<DeliverymanDomain> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
