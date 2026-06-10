using MediatR;

namespace SmartLoan.Application.Commands.SubmitLoan;

public record SubmitLoanCommand(
    string ApplicantName,
    string ApplicantEmail,
    decimal RequestedAmount,
    decimal AnnualIncome,
    decimal MonthlyDebt) : IRequest<Guid> ;