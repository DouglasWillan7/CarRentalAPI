using DW.Rental.Data.Entities;
using DW.Rental.Domain.Domains;
using DW.Rental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DW.Rental.Data.Repositories;

public class DeliverymanRepository : IDeliverymanRepository
{
    private DatabaseContext.LocationDbContext _dbContext { get; }

    public DeliverymanRepository(DatabaseContext.LocationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeliverymanDomain> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var delivery = await _dbContext.Deliveryman.Where(m => m.Id == id).FirstOrDefaultAsync(cancellationToken);

        if (delivery is null)
            return null;

        return (DeliverymanDomain)delivery; 
    }

    public async Task<DeliverymanDomain> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var delivery = await _dbContext.Deliveryman.Where(m => m.UserId == userId).FirstOrDefaultAsync(cancellationToken);

        if (delivery is null)
            return null;

        return (DeliverymanDomain)delivery;
    }

    public async Task<DeliverymanDomain> GetByCnhAsync(int cnh, CancellationToken cancellationToken)
    {
        var delivery = await _dbContext.Deliveryman.Where(m => m.CNH == cnh).FirstOrDefaultAsync(cancellationToken);

        if (delivery is null)
            return null;

        return (DeliverymanDomain)delivery;
    }

    public async Task<DeliverymanDomain> CreateAsync(DeliverymanDomain motorcycleDomain, CancellationToken cancellationToken)
    {
        var user = new User { Username = motorcycleDomain.Name, Role = Shareable.Enum.RoleTypeEnum.Deliveryman.ToString(), Password = motorcycleDomain.Password, Status = Domain.Enums.Status.Ativo };

        await _dbContext.User.AddAsync(user, cancellationToken);

        var deliveryman = new Deliveryman(motorcycleDomain, user);

        var createdDeliveryman = await _dbContext.Deliveryman.AddAsync(deliveryman, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return (DeliverymanDomain)createdDeliveryman.Entity;
    }

    public async Task<DeliverymanDomain> UpdateFotoAsync(Guid deliverymanId, string photoName, CancellationToken cancellationToken)
    {
        var createdDeliveryman = await _dbContext.Deliveryman.Where(x=>x.Id == deliverymanId).FirstOrDefaultAsync();
        if (createdDeliveryman is null)
            return null;

        createdDeliveryman.CnhPhoto = photoName;
        _dbContext.Deliveryman.Update(createdDeliveryman);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return (DeliverymanDomain)createdDeliveryman;
    }

    public Task DeleteAsync(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
