using SmartLoan.Domain.Entities;

namespace SmartLoan.Application.Common.Interfaces;

public interface ILoanRepository
{
    Task AddAsync(LoanApplication loan, CancellationToken ct = default);
    Task<LoanApplication?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task SaveChangesAsync (CancellationToken ct = default);
}