using SmartLoan.Domain.Entities;

namespace SmartLoan.Application.Services;

public class EligibilityResult
{
    public bool IsEligibile{get; }
    public string Reason{get; } = null!;

    private EligibilityResult(bool isEligibile, string reason)
    {
        IsEligibile = isEligibile;
        Reason = reason;
    }

    public static EligibilityResult Pass() =>
        new (true, string.Empty);

    public static EligibilityResult Fail(string reason) =>
        new(false, reason);
}

public class EligibilityService
{
    public EligibilityResult Check(
        decimal requestedAmount,
        decimal annualIncome,
        decimal monthlyDebt
    )
    {
        if(requestedAmount <= 0)
            return EligibilityResult.Fail("Requested amount must be greater than zero.");

        if(annualIncome <= 0)
            return EligibilityResult.Fail("Annual income must be greater than zero.");

        decimal monthlyIncome = annualIncome /12;
        decimal dti = monthlyDebt / monthlyIncome;

        if (dti >= 0.43m)
            return EligibilityResult.Fail($"DTI of {dti:P0} exceeds the 43% threshold.");

        return EligibilityResult.Pass();
    }
}