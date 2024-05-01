using DW.Rental.Domain.Domains;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW.Rental.Data.Entities;

[Table(name: "Motocycle", Schema = "dbo")]
[Index(nameof(Id), IsUnique = true)]
public class Motorcycle : BaseModel
{
    [Required]
    public int Year { get; set; }

    [Required]
    public string LicensePlate { get; set; } = string.Empty;

    [Required]
    public string Model { get; set; } = string.Empty;

    public bool Disponivel { get; set; } = true;
    public Rental Rental { get; set; }


    public void Create(int year, string licensePlate, string model)
    {
        Year = year;
        LicensePlate = licensePlate;
        Model = model;
    }


    public static implicit operator Motorcycle(MotorcycleDomain motorcycle) =>
        new MotorcycleDomain(motorcycle.Id, motorcycle.Year, motorcycle.LicensePlate, motorcycle.Model);

    public static implicit operator MotorcycleDomain(Motorcycle motorcycle) =>
       new MotorcycleDomain(motorcycle.Id, motorcycle.Year, motorcycle.LicensePlate, motorcycle.Model);
}
