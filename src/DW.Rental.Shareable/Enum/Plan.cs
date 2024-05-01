namespace DW.Rental.Shareable.Enum;

public static class Plan
{
    public static Dictionary<PlanEnum, double> PlanList()
    {
        var planList = new Dictionary<PlanEnum, double>
        {
            { PlanEnum.semanal, 30 },
            { PlanEnum.quizenal, 28 },
            { PlanEnum.mensal, 22 },
            { PlanEnum.quarentaecincodias, 20 },
            { PlanEnum.cinquentadias, 18 }
        };

        return planList;
    }
}
