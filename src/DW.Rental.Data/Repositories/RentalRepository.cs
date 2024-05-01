using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Repositories;

namespace DW.Rental.Data.Repositories;

public class RentalRepository : IRentalRepository
{
    private DatabaseContext.LocationDbContext _dbContext { get; }

    public RentalRepository(DatabaseContext.LocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RentalDomain> CreateAsync(RentalDomain rentalDomain)
    {
        try
        {
            var rental = new Entities.Rental(
            rentalDomain.DeliverymanId,
            rentalDomain.MotorcycleId,
            rentalDomain.DataInicio,
            rentalDomain.DataFim,
            rentalDomain.Plan,
            rentalDomain.ValorTotal);

            var createdRental = await _dbContext.Rental.AddAsync(rental);

            await _dbContext.SaveChangesAsync();

            return (RentalDomain)createdRental.Entity;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
