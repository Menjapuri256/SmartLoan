using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLoan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlyDebtToLoanApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyDebt",
                table: "LoanApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyDebt",
                table: "LoanApplications");
        }
    }
}
