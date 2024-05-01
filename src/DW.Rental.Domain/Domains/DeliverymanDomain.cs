using DW.Rental.Shareable.Enum;
using DW.Rental.Shareable.Requests.Deliveryman;
using DW.Rental.Shareable.Responses.Deliveryman;

namespace DW.Rental.Domain.Domains;

public class DeliverymanDomain
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;

    public DateOnly Birthday { get; set; }

    public int CNH { get; set; }

    public CnhTypeEnum CnhType { get; set; }

    public string CnhPhoto { get; set; } = string.Empty;


    public static implicit operator CreateDeliverymanResponse(DeliverymanDomain deliverymanDomain)
        => new CreateDeliverymanResponse(deliverymanDomain.Id, deliverymanDomain.Name, deliverymanDomain.Cnpj, deliverymanDomain.Birthday, deliverymanDomain.CNH, deliverymanDomain.CnhType);

    public static implicit operator UpdateDeliverymanResponse(DeliverymanDomain deliverymanDomain)
        => new UpdateDeliverymanResponse(deliverymanDomain.Id, deliverymanDomain.Name, deliverymanDomain.Cnpj, deliverymanDomain.Birthday, deliverymanDomain.CNH, deliverymanDomain.CnhType);

    public static implicit operator DeliverymanDomain(CreateDeliverymanRequest createDeliverymanRequest)
        => new DeliverymanDomain
        {
            CnhType = createDeliverymanRequest.CnhType,
            CNH = createDeliverymanRequest.CnhNumber,
            Password = createDeliverymanRequest.Password,
            Birthday = createDeliverymanRequest.Birthday,
            Cnpj = createDeliverymanRequest.Cnpj,
            Name = createDeliverymanRequest.Name
        };
}
