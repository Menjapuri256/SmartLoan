using System;
using SmartLoan.Domain.Enums;
using SmartLoan.Domain.ValueObjects;

namespace SmartLoan.Domain.Entities;

public class LoanApplication
{
    public Guid Id {get; private set;} 

    public string ApplicantName {get; private set;} = null!;

    public string ApplicantEmail {get; private set;} = null!;

    public Money RequestedAmount {get; private set;} = null!;

    public Money AnnualIncome {get; private set;} = null!;

    public decimal MonthlyDebt {get; private set;}

    public LoanStatus Status{get; private set;}

    public DateTime CreatedAt{get; private set;}

    public DateTime UpdatedAt{get; private set;}

    private LoanApplication() { }
    
    public static LoanApplication Create(
        string applicantName,
        string applicantEmail,
        Money requestedAmount,
        Money annualIncome,
        decimal monthlyDebt
    )
    {
        return new LoanApplication
        {
            Id = Guid.NewGuid(),
            ApplicantName = applicantName,
            ApplicantEmail = applicantEmail,
            RequestedAmount = requestedAmount,
            AnnualIncome = annualIncome,
            MonthlyDebt = monthlyDebt,
            Status = LoanStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow 
        };
    }

    public void Approve()
    {
        Status = LoanStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = LoanStatus.Rejected;
        UpdatedAt = DateTime.UtcNow;
    }
}