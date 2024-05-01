using DW.Rental.Domain.Domains;
using DW.Rental.Shareable.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW.Rental.Data.Entities;

[Table(name: "Rental", Schema = "dbo")]
[Index(nameof(Id), IsUnique = true)]
public class Rental : BaseModel
{
    [Required]
    public Guid DeliverymanId { get; set; }
    public Deliveryman Deliveryman { get; private set; }

    [Required]
    public Guid MotorcycleId { get; set; }
    public Motorcycle Motorcycle { get; private set; }

    [Required]
    public DateOnly DataInicio { get; set; }
    [Required]
    public DateOnly DataFim { get; set; }
    [Required]
    public PlanEnum Plan { get; set; }
    [Required]
    public decimal ValorTotal { get; set; }

    public Rental()
    {

    }

    public Rental(Guid deliverymanId, Guid motorcycleId, DateOnly dataInicio, DateOnly dataFim, PlanEnum plan, decimal valorTotal)
    {
        DeliverymanId = deliverymanId;
        MotorcycleId = motorcycleId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Plan = plan;
        ValorTotal = valorTotal;
    }

    public static implicit operator Rental(RentalDomain rental) =>
        new RentalDomain(rental.Id, rental.Plan, rental.DeliverymanId, rental.MotorcycleId, rental.DataInicio, rental.DataFim);

    public static implicit operator RentalDomain(Rental rental) =>
       new RentalDomain(rental.Id, rental.Plan, rental.DeliverymanId, rental.MotorcycleId, rental.DataInicio, rental.DataFim);
}
