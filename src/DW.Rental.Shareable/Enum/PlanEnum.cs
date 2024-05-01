using System.ComponentModel.DataAnnotations;

namespace DW.Rental.Shareable.Enum;

public enum PlanEnum
{
    [Display(Name = nameof(semanal))]
    semanal,
    [Display(Name = nameof(quizenal))]
    quizenal,
    [Display(Name = nameof(mensal))]
    mensal,
    [Display(Name = nameof(quarentaecincodias))]
    quarentaecincodias,
    [Display(Name = nameof(cinquentadias))]
    cinquentadias,
}
