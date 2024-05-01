using DW.Rental.Domain.Domains;

namespace DW.Rental.Domain.Repositories;

public interface IUserRepository
{
    Task<UserDomain?> GetAsync(string username, string password, CancellationToken cancellationToken);
    Task DeleteAsync(string placa, CancellationToken cancellationToken);
}
