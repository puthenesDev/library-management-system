using LibraryManagement.Application.Contracts.Loans;
using LibraryManagement.Application.Loans.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Loans;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(LoanResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Borrow([FromBody] BorrowBookRequest request)
    {
        var command = new BorrowBookCommand(request.BookId, request.MemberId);
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id:guid}/return")]
    [ProducesResponseType(typeof(LoanResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Return(Guid id)
    {
        var command = new ReturnBookCommand(id);
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        return NotFound();
    }


    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLoanRequest request)
    {
        var command = new UpdateLoanCommand(id, request.DueDate);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteLoanCommand(id);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();
        return NoContent();
    }
}