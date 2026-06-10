using MediatR;
using SmartLoan.Application.Common.DTOs;

namespace SmartLoan.Application.Queries.GetLoan;

public record GetLoanQuery(Guid Id) : IRequest<LoanApplicationDto>;