# SmartLoan — Loan Origination System

A portfolio project demonstrating production-grade .NET 8 architecture for a Loan Origination System (LOS). Built with Clean Architecture, CQRS, MediatR, and Entity Framework Core.

> **Phase 1** — REST API with SQLite. Phase 2 (Microservices) and Phase 3 (RAG / Azure AI) are in progress.

---

## Architecture Overview

SmartLoan follows **Clean Architecture** — dependencies point inward. The domain has no knowledge of the database, HTTP, or any infrastructure concern.

```
SmartLoan.Api              ← ASP.NET Core Web API (entry point, controllers, DI wiring)
    └── SmartLoan.Application  ← Use cases: Commands, Queries, Handlers, Interfaces
            └── SmartLoan.Domain   ← Entities, Value Objects, Domain Rules (no dependencies)
SmartLoan.Infrastructure   ← EF Core, SQLite, Repository implementations
```

### Dependency Rule

```
Api → Application → Domain
Infrastructure → Application (implements interfaces defined there)
```

`Domain` and `Application` have zero references to Infrastructure or ASP.NET. This means the core business logic is fully testable in isolation.

---

## Key Patterns

### CQRS with MediatR

Commands and Queries are separated into distinct request/handler pairs via MediatR. Controllers dispatch requests — they contain no business logic.

```
POST /loans  →  SubmitLoanCommand  →  SubmitLoanCommandHandler
GET  /loans  →  GetLoansQuery      →  GetLoansQueryHandler
```

### Rich Domain Model

Entities enforce their own invariants through private setters and factory methods. Business rules live in the domain — not in services or handlers.

```csharp
// Example: LoanApplication is only created via a controlled factory method
var loan = LoanApplication.Create(applicantName, amount, purpose);
```

### Value Objects

Monetary amounts are represented as a `Money` value object with proper `Equals` / `GetHashCode` overrides. Two `Money` instances with the same amount are equal by value, not by reference.

### Repository Pattern

`ILoanRepository` is defined in `Application`. `LoanRepository` (EF Core implementation) lives in `Infrastructure`. The application layer never touches EF Core directly.

### Result Object

Handlers return a `Result<T>` type with static factory methods (`Pass()` / `Fail()`) instead of throwing exceptions for expected failures, keeping control flow explicit.

---

## Project Structure

```
SmartLoan/
├── SmartLoan.Domain/
│   ├── Entities/
│   │   └── LoanApplication.cs
│   ├── ValueObjects/
│   │   └── Money.cs
│   └── Enums/
│       └── LoanStatus.cs
│
├── SmartLoan.Application/
│   ├── Commands/
│   │   └── SubmitLoan/
│   │       ├── SubmitLoanCommand.cs
│   │       └── SubmitLoanCommandHandler.cs
│   ├── Queries/
│   │   └── GetLoans/
│   │       ├── GetLoansQuery.cs
│   │       └── GetLoansQueryHandler.cs
│   ├── DTOs/
│   │   └── LoanApplicationDto.cs
│   ├── Interfaces/
│   │   └── ILoanRepository.cs
│   └── Services/
│       └── EligibilityService.cs
│
├── SmartLoan.Infrastructure/
│   ├── Persistence/
│   │   ├── SmartLoanDbContext.cs
│   │   ├── SmartLoanDbContextFactory.cs
│   │   └── LoanRepository.cs
│   └── DependencyInjection.cs
│
└── SmartLoan.Api/
    ├── Controllers/
    │   └── LoansController.cs
    ├── Program.cs
    └── appsettings.json
```

---

## Tech Stack

| Concern | Technology |
|---|---|
| Framework | .NET 8 / ASP.NET Core |
| Language | C# |
| Mediator | MediatR |
| ORM | Entity Framework Core |
| Database (local) | SQLite |
| Database (cloud) | Azure SQL *(Phase 2+)* |
| Architecture | Clean Architecture, CQRS |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install EF tools if you haven't:
```bash
dotnet tool install --global dotnet-ef
```

### 1. Clone the repo

```bash
git clone https://github.com/YOUR_USERNAME/SmartLoan.git
cd SmartLoan
```

### 2. Generate the database migration

Migrations are not committed to source control. Generate them locally:

```bash
dotnet ef migrations add InitialCreate --project SmartLoan.Infrastructure --startup-project SmartLoan.Api
```

### 3. Apply the migration

```bash
dotnet ef database update --project SmartLoan.Infrastructure --startup-project SmartLoan.Api
```

This creates a local `smartloan.db` SQLite file.

### 4. Run the API

```bash
cd SmartLoan.Api
dotnet run
```

The API starts at `https://localhost:5001` (or the port shown in your terminal).

### 5. Explore with Swagger

Navigate to:
```
https://localhost:5001/swagger
```

---

## API Endpoints

| Method | Route | Description |
|---|---|---|
| `POST` | `/api/loans` | Submit a new loan application |
| `GET` | `/api/loans` | Retrieve all loan applications |
| `GET` | `/api/loans/{id}` | Retrieve a loan application by ID |
| `PUT` | `/api/loans/{id}/status` | Update loan status |

### Sample Request — Submit Loan

```json
POST /api/loans
{
  "applicantName": "Jane Smith",
  "amount": 250000,
  "purpose": "HomePurchase",
  "annualIncome": 90000,
  "monthlyDebt": 1200
}
```

---

## Domain Concepts

SmartLoan models a simplified mortgage loan origination workflow.

**Loan Lifecycle:**
```
Submitted → UnderReview → Approved / Denied
```

**Eligibility Rules (Phase 1):**
- Debt-to-Income ratio (DTI) must be ≤ 43%
- Loan amount must be > $0
- Applicant name must be provided

---

## Roadmap

- [x] **Phase 1** — Clean Architecture REST API (this repo)
- [ ] **Phase 2** — Microservices (Azure Service Bus, Docker, API Gateway)
- [ ] **Phase 3** — RAG pipeline (Semantic Kernel, Azure AI Search, Azure OpenAI)

---

## Design Decisions

**Why Clean Architecture?**
Keeps business logic independent of frameworks and infrastructure. The domain can be unit tested without spinning up a database or web server.

**Why CQRS?**
Separating reads from writes makes each handler focused on one job. It also opens the door to separate read/write models in Phase 2.

**Why MediatR?**
Decouples controllers from handlers entirely. Adding a new use case means adding a new Command/Handler pair — no changes to existing code.

**Why SQLite for Phase 1?**
Zero infrastructure setup locally. The `DbContext` and repository are abstracted behind interfaces, so swapping in Azure SQL for Phase 2 requires only a connection string change and a new migration.

---

## Author

Built as a portfolio project demonstrating modern .NET architecture for enterprise financial applications.
