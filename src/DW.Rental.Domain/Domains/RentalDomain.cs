using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Messages;

namespace DW.Rental.Domain.Domains;

public class RentalDomain
{
    public RentalDomain(Guid id, PlanEnum plan, Guid deliverymanId, Guid motorcycleId, DateOnly dataInicio, DateOnly dataFim)
    {
        Id = id;
        Plan = plan;
        DeliverymanId = deliverymanId;
        MotorcycleId = motorcycleId;
        DataInicio = dataInicio;
        DataFim = dataFim;
    }

    public Guid Id { get; set; }
    public Guid DeliverymanId { get; set; }
    public DeliverymanDomain Deliveryman { get; private set; }
    public Guid MotorcycleId { get; set; }
    public MotorcycleDomain Motorcycle { get; private set; }
    public DateOnly DataInicio { get; set; }
    public DateOnly DataFim { get; set; }
    public PlanEnum Plan { get; set; }
    public decimal ValorTotal { get; set; }

    public static explicit operator RentalDomain(RentalMessage rentalMessage)
    {
        return new RentalDomain(rentalMessage.Id, rentalMessage.Plan, rentalMessage.DeliverymanId, rentalMessage.MotorcycleId, rentalMessage.DataInicial, rentalMessage.DataFinalPrevista);
    }
}
