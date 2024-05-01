using DW.Rental.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace DW.Rental.Data.DatabaseContext;

public class LocationDbContext : DbContext
{
    public LocationDbContext(DbContextOptions<LocationDbContext> options)
       : base(options)
    {
    }

    public virtual DbSet<Motorcycle> Motorcycle { get; set; }
    public virtual DbSet<Deliveryman> Deliveryman { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Entities.Rental> Rental { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationDbContext).Assembly);

        modelBuilder.Entity<Motorcycle>()
          .HasIndex(e => e.LicensePlate)
          .IsUnique();

        modelBuilder.Entity<Deliveryman>()
            .HasIndex(e => e.Cnpj)
            .IsUnique();

        modelBuilder.Entity<Deliveryman>()
            .HasIndex(e => e.CNH)
            .IsUnique();

        modelBuilder.Entity<Deliveryman>()
          .HasOne(e => e.User)
          .WithOne(u => u.Deliveryman)
          .HasForeignKey<Deliveryman>(e => e.UserId);

        modelBuilder.Entity<Entities.Rental>()
          .HasOne(e => e.Deliveryman)
          .WithOne(u => u.Rental)
          .HasForeignKey<Entities.Rental>(e => e.DeliverymanId);

        modelBuilder.Entity<Entities.Rental>()
          .HasOne(e => e.Motorcycle)
          .WithOne(u => u.Rental)
          .HasForeignKey<Entities.Rental>(e => e.MotorcycleId);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        OnSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnSaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added when entry.Entity is BaseModel entity:
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                        break;
                    }
                case EntityState.Modified when entry.Entity is BaseModel entity:
                    {
                        entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
