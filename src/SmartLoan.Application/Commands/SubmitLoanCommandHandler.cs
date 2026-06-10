using MediatR;
using SmartLoan.Application.Common.Interfaces;
using SmartLoan.Application.Services;
using SmartLoan.Domain.ValueObjects;
using SmartLoan.Domain.Entities;

namespace SmartLoan.Application.Commands.SubmitLoan;

public class SubmitLoanCommandHandler : IRequestHandler<SubmitLoanCommand, Guid>
{
    private readonly ILoanRepository _repository;
    private readonly EligibilityService _eligibilityService;

    public SubmitLoanCommandHandler(ILoanRepository repository, EligibilityService eligibilityService)
    {
        _repository =repository;
        _eligibilityService = eligibilityService;
    }
    public async Task<Guid> Handle(SubmitLoanCommand request, CancellationToken ct)
    {
        var eligibility = _eligibilityService.Check(
            request.RequestedAmount,
            request.AnnualIncome,
            request.MonthlyDebt);

         if (!eligibility.IsEligibile)
            throw new InvalidOperationException(eligibility.Reason);

        var loan =  LoanApplication.Create(
            request.ApplicantName,
            request.ApplicantEmail,
            new Money(request.RequestedAmount),
            new Money(request.AnnualIncome),
            request.MonthlyDebt
        );

        await _repository.AddAsync(loan, ct);

        return loan.Id;
    }
}