using Microsoft.EntityFrameworkCore;
using SmartLoan.Domain.Entities;

namespace SmartLoan.Infrastructure.Persistence;

public class SmartLoanDbContext : DbContext
{
    public SmartLoanDbContext(DbContextOptions<SmartLoanDbContext> options) : base(options)
    {
    }
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartLoanDbContext).Assembly);
    }
}