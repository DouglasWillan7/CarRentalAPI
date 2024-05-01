using DW.Rental.Shareable.Enum;

namespace DW.Rental.Shareable.Responses.Deliveryman;

public record UpdateDeliverymanResponse(Guid Id, string Nome, string Cnpj, DateOnly DataNascimento, int NumeroCnh, CnhTypeEnum TipoCnh);
