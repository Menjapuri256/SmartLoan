using MediatR;
using SmartLoan.Application.Common.DTOs;
using SmartLoan.Application.Common.Interfaces;

namespace SmartLoan.Application.Queries.GetLoan;

public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, LoanApplicationDto>
{
    private readonly ILoanRepository _repository;
    public GetLoanQueryHandler(ILoanRepository repository)
    {
    _repository = repository;    
    }

    public async Task<LoanApplicationDto> Handle(GetLoanQuery qry, CancellationToken ct)
    {
       var loan = await _repository.GetByIdAsync(qry.Id, ct);

       if (loan is null)
        throw new KeyNotFoundException($"Loan {qry.Id} not found");

        var dto = new LoanApplicationDto(
           loan.Id,
           loan.ApplicantName,
           loan.ApplicantEmail,
           loan.RequestedAmount.Amount,
           loan.AnnualIncome.Amount,
           loan.Status.ToString(),
           loan.CreatedAt,
           loan.UpdatedAt
        );

        return dto;
    }
}