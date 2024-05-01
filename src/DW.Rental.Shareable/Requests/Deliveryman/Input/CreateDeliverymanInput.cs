using DW.Rental.Shareable.Enum;

namespace DW.Rental.Shareable.Requests.Deliveryman.Input;

public record CreateDeliverymanInput(string Nome, string Senha, string Cnpj, DateTime DataNascimento, int NumeroCnh, CnhTypeEnum TipoCnh);
