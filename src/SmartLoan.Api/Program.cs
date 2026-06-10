using Microsoft.EntityFrameworkCore;
using SmartLoan.Application.Common.Interfaces;
using SmartLoan.Infrastructure.Persistence;
using SmartLoan.Infrastructure.Persistence.Repositories;
using SmartLoan.Application.Services;
using SmartLoan.Application.Commands.SubmitLoan;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<EligibilityService>();
builder.Services.AddControllers();

builder.Services.AddDbContext<SmartLoanDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(SubmitLoanCommand).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();