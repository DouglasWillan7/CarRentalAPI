using DW.Rental.Domain.Domains;
using DW.Rental.Shareable.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW.Rental.Data.Entities;

[Table(name: "Deliveryman", Schema = "dbo")]
[Index(nameof(Id), IsUnique = true)]
public class Deliveryman : BaseModel
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Cnpj { get; set; } = string.Empty;

    [Required]
    public DateOnly Birthday { get; set; }

    [Required]
    public int CNH { get; set; }

    [Required]
    public CnhTypeEnum CnhType { get; set; }

    public string CnhPhoto { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public User User { get; private set; }
    public Rental Rental { get; private set; }

    public Deliveryman()
    {
            
    }
    public Deliveryman(DeliverymanDomain deliverymanDomain, User user = null)
    {
        Name = deliverymanDomain.Name;
        Cnpj = deliverymanDomain.Cnpj;
        Birthday = deliverymanDomain.Birthday;
        CNH = deliverymanDomain.CNH;
        CnhType = deliverymanDomain.CnhType;
        CnhPhoto = deliverymanDomain.CnhPhoto;
        UserId = user.Id;
        Status = Domain.Enums.Status.Ativo;
    }

    public static implicit operator Deliveryman(DeliverymanDomain deliveryman) =>
        new DeliverymanDomain
        {
            Id = deliveryman.Id,
            CNH = deliveryman.CNH,
            Cnpj = deliveryman.Cnpj,
            Birthday = deliveryman.Birthday,
            CnhPhoto = deliveryman.CnhPhoto,
            Name = deliveryman.Name,
            CnhType = deliveryman.CnhType
        };

    public static implicit operator DeliverymanDomain(Deliveryman deliveryman) =>
       new DeliverymanDomain
       {
           Id = deliveryman.Id,
           CNH = deliveryman.CNH,
           Cnpj = deliveryman.Cnpj,
           Birthday = deliveryman.Birthday,
           CnhPhoto = deliveryman.CnhPhoto,
           Name = deliveryman.Name,
           CnhType = deliveryman.CnhType
       };
}
