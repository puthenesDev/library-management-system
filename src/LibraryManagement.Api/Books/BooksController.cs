using LibraryManagement.Application.Books.Commands;
using LibraryManagement.Application.Books.Queries;
using LibraryManagement.Application.Contracts.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Books;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        var command = new CreateBookCommand(
            request.LibraryId,
            request.Isbn,
            request.Title,
            request.Author,
            request.TotalCopies);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetBookByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetBooksQuery());
        return Ok(result);
    }

    // PUT: api/books/{id}
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookRequest request)
    {
        var command = new UpdateBookCommand(
            id,
            request.Isbn,
            request.Title,
            request.Author,
            request.TotalCopies);

        var success = await _mediator.Send(command);

        if (!success) return NotFound();

        return NoContent();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand(id);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();

        return NoContent();
    }
}
