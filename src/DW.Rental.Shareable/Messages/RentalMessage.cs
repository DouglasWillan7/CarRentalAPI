using DW.Rental.Shareable.Enum;

namespace DW.Rental.Shareable.Messages;

public record RentalMessage(Guid Id, DateOnly DataInicial, DateOnly DataFinalPrevista, PlanEnum Plan, Guid DeliverymanId, Guid MotorcycleId, decimal ValorTotal);