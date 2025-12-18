using LibraryManagement.Application.Contracts.Libraries;
using LibraryManagement.Application.Libraries.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Libraries;


[ApiController]
[Route("api/[controller]")]
public class LibrariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibrariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(LibraryResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateLibraryRequest request)
    {
        var command = new CreateLibraryCommand(request.Name, request.Address);
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLibraryRequest request)
    {
        var command = new UpdateLibraryCommand(id, request.Name, request.Address);
        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteLibraryCommand(id);
        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

}