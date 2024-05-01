using DW.Rental.Shareable.Enum;

namespace DW.Rental.Domain.Domains;

public class UserDomain
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public RoleTypeEnum Role { get; set; }
    public DeliverymanDomain? DeliverymanDomain { get; set;}
}
