using Bogus;
using DW.Rental.Domain.Domains;

namespace Dw.Rental.Test.DataFaker.Motorcycle;

public class MotorcycleFaker : Faker<MotorcycleDomain>
{
    public MotorcycleFaker()
    {
        RuleFor(m => m.Id, Guid.NewGuid());
        RuleFor(m => m.LicensePlate, p => p.Random.Word());
        RuleFor(m => m.Model, p => p.Vehicle.Model());
        RuleFor(m => m.Year, p => p.Date.Between(new DateTime(2000, 01, 01), new DateTime(2024, 12, 31)).Year);
    }
}
