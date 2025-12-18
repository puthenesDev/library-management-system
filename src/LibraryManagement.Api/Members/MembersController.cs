using LibraryManagement.Application.Contracts.Members;
using LibraryManagement.Application.Members.Commands;
using LibraryManagement.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Members;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateMemberRequest request)
    {
        var command = new CreateMemberCommand(request.Name, request.Email);
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetMemberByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MemberResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetMembersQuery());
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMemberRequest request)
    {
        var command = new UpdateMemberCommand(id, request.Name, request.Email);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteMemberCommand(id);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();
        return NoContent();
    }
}