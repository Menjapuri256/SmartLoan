using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLoan.Domain.Entities;
using SmartLoan.Domain.ValueObjects;

namespace SmartLoan.Infrastructure.Persistence.Configurations;

public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
{
    public void Configure(EntityTypeBuilder<LoanApplication> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ApplicantName);
        builder.Property(x => x.ApplicantEmail);
        builder.OwnsOne(x => x.RequestedAmount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("RequestedAmount");
        });
        builder.OwnsOne(x => x.AnnualIncome, money =>
        {
            money.Property(m => m.Amount).HasColumnName("AnnualIncome");
        });
        builder.Property(x => x.Status)
               .HasConversion<string>();
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
    }
}
