using DW.Rental.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW.Rental.Data.Entities;

public class BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [DefaultValue(1)]
    [Display(Name = "Status")]
    public Status Status { get; set; }

    [Required]
    [Display(Name = "CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "UpdatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "DeletedAt")]
    public DateTime? DeletedAt { get; set; }
}
