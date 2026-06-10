// SmartLoan.Infrastructure/Persistence/SmartLoanDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartLoan.Infrastructure.Persistence;

public class SmartLoanDbContextFactory : IDesignTimeDbContextFactory<SmartLoanDbContext>
{
    public SmartLoanDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SmartLoanDbContext>()
            .UseSqlite("Data Source=smartloan.db")
            .Options;

        return new SmartLoanDbContext(options);
    }
}