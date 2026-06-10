
using SmartLoan.Domain.Entities;
using SmartLoan.Application.Common.Interfaces;

namespace SmartLoan.Infrastructure.Persistence.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly SmartLoanDbContext _dbContext;
    public LoanRepository(SmartLoanDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(LoanApplication loan, CancellationToken ct = default)
    {
        await _dbContext.LoanApplications.AddAsync(loan, ct);
    }
    public async Task<LoanApplication?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
       return await _dbContext.LoanApplications.FindAsync(id, ct);
    }
    public async Task SaveChangesAsync (CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}