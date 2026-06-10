using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using SmartLoan.Application.Commands.SubmitLoan;
using SmartLoan.Application.Queries.GetLoan;

namespace SmartLoan.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitLoan([FromBody] SubmitLoanCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetLoan(Guid id)
    {
        var result = await _mediator.Send(new GetLoanQuery(id));

        if (result is null)
            return NotFound();
            
        return Ok(result);
    }
} 