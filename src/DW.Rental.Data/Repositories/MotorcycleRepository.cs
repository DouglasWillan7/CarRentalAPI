using DW.Rental.Data.Entities;
using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Enums;
using DW.Rental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DW.Rental.Data.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private DatabaseContext.LocationDbContext _dbContext { get; }

    public MotorcycleRepository(DatabaseContext.LocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MotorcycleDomain> CreateAsync(MotorcycleDomain motorcycleDomain, CancellationToken cancellationToken)
    {
        var motorcycle = new Motorcycle();
        motorcycle.Create(motorcycleDomain.Year, motorcycleDomain.LicensePlate, motorcycleDomain.Model);

        var createdMotorcycle = await _dbContext.Motorcycle.AddAsync(motorcycle, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return (MotorcycleDomain)createdMotorcycle.Entity;
    }

    public async Task<MotorcycleDomain?> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken)
    {
        var motorcycle = await _dbContext.Motorcycle.Where(m => m.LicensePlate == licensePlate).FirstOrDefaultAsync(cancellationToken);

        if (motorcycle is null)
            return null;

        return (MotorcycleDomain)motorcycle;
    }

    public async Task DeleteAsync(string licensePlate, CancellationToken cancellationToken)
    {
        var motorcycle = await _dbContext.Motorcycle.Where(x => x.LicensePlate == licensePlate).ExecuteDeleteAsync(cancellationToken);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<MotorcycleDomain> UpdateLicensePlateAsync(string oldLicensePlate, string newLicensePlate, CancellationToken cancellationToken)
    {
        var motorcycle = await _dbContext.Motorcycle.Where(x => x.LicensePlate == oldLicensePlate).FirstOrDefaultAsync();

        if (motorcycle is null)
            return null;

        motorcycle.LicensePlate = newLicensePlate;

        var updatedMotorcycle = _dbContext.Update(motorcycle);

        await _dbContext.SaveChangesAsync();

        return (MotorcycleDomain)updatedMotorcycle.Entity;
    }

    public async Task<MotorcycleDomain?> GetAvailableAsync(CancellationToken cancellationToken)
    {
        var motorcycle = await _dbContext.Motorcycle.Where(x => x.Disponivel == true).FirstOrDefaultAsync();

        if (motorcycle is null)
            return null;

        return (MotorcycleDomain)motorcycle;
    }

    public async Task<MotorcycleDomain> UpdateAvailable(Guid motorcycleId)
    {
        var motorcycle = await _dbContext.Motorcycle.Where(x => x.Id == motorcycleId).FirstOrDefaultAsync();

        if (motorcycle is null)
            return null;

        motorcycle.Status = Status.Inativo;

        var updatedMotorcycle = _dbContext.Update(motorcycle);

        await _dbContext.SaveChangesAsync();


        return (MotorcycleDomain)updatedMotorcycle.Entity;
    }
}
