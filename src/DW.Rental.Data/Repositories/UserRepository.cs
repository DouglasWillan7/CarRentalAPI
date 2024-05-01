using DW.Rental.Data.DatabaseContext;
using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DW.Rental.Data.Repositories;

public class UserRepository : IUserRepository
{
    private LocationDbContext _dbContext { get; }

    public UserRepository(LocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MotorcycleDomain> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken)
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

    public async Task<UserDomain?> GetAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _dbContext.User.Where(user => user.Username == username && user.Password == password).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
            return null;

        return (UserDomain)user;
    }
}
