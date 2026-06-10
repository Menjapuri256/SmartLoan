namespace SmartLoan.Application.Common.DTOs;

public record LoanApplicationDto(
    Guid Id,
    string ApplicantName,
    string ApplicantEmail,
    decimal RequestedAmount,
    decimal AnnualIncome, 
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);