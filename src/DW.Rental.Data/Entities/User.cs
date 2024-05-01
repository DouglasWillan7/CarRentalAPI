using DW.Rental.Domain.Domains;
using DW.Rental.Shareable.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW.Rental.Data.Entities;

[Table(name: "User", Schema = "dbo")]
[Index(nameof(Id), IsUnique = true)]
public class User : BaseModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;

    public Deliveryman Deliveryman { get; set; }

    public User()
    {

    }

    public void Create(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public static implicit operator User(UserDomain user) =>
       new UserDomain { Username = user.Username, Password = user.Password, Role = user.Role };

    public static implicit operator UserDomain(User user) =>
       new UserDomain { Id = user.Id, Username = user.Username, Password = user.Password, Role = Enum.Parse<RoleTypeEnum>(user.Role) };
}
