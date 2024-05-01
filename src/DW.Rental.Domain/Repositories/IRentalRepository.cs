using DW.Rental.Domain.Domains;

namespace DW.Rental.Domain.Repositories;

public interface IRentalRepository
{
    Task<RentalDomain> CreateAsync(RentalDomain motorcycleDomain);
}
