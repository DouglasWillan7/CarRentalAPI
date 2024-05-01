using DW.Rental.Shareable.Enum;

namespace DW.Rental.Shareable.Responses.Deliveryman;

public record CreateDeliverymanResponse(Guid Id, string Name, string Cnpj, DateOnly Birthday, int CnhNumber, CnhTypeEnum CnhType);
