using DW.Rental.Shareable.Requests.Motorcycle;
using DW.Rental.Shareable.Responses.Motorcycle;

namespace DW.Rental.Domain.Domains;

public class MotorcycleDomain
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;

    public MotorcycleDomain(Guid id,int year, string licensePlate, string model)
    {
        Id = id;
        Year = year;
        LicensePlate = licensePlate;
        Model = model;
    }

    public MotorcycleDomain(int year, string licensePlate, string model)
    {
        Year = year;
        LicensePlate = licensePlate;
        Model = model;
    }

    public static implicit operator CreateMotorcycleResponse(MotorcycleDomain motorcycleDomain)
        => new CreateMotorcycleResponse(motorcycleDomain.Id, motorcycleDomain.LicensePlate, motorcycleDomain.Model, motorcycleDomain.Year);

    public static implicit operator GetMotorcycleResponse(MotorcycleDomain motorcycleDomain)
        => new GetMotorcycleResponse(motorcycleDomain.Id, motorcycleDomain.LicensePlate, motorcycleDomain.Model, motorcycleDomain.Year);

    public static implicit operator UpdateMotorcycleResponse(MotorcycleDomain motorcycleDomain)
        => new UpdateMotorcycleResponse(motorcycleDomain.Id, motorcycleDomain.LicensePlate, motorcycleDomain.Model, motorcycleDomain.Year);

    public static implicit operator MotorcycleDomain(CreateMotorcycleRequest createMotoRequest)
        => new MotorcycleDomain(createMotoRequest.Year, createMotoRequest.LicensePlate, createMotoRequest.Model);
}
