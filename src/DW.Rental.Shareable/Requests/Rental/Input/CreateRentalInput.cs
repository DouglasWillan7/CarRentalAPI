using DW.Rental.Shareable.Enum;

namespace DW.Rental.Shareable.Requests.Rental.Input;

public record CreateRentalInput(DateTime DataInicio, DateTime DataPrevistaEntrega, string CnpjEntregador, PlanEnum PlanEnum);
