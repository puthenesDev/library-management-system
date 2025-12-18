using LibraryManagement.Application.Contracts.Books;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Books.Queries;

public sealed class GetBookByIdQueryHandler
    : IRequestHandler<GetBookByIdQuery, BookResponse?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookResponse?> Handle(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (book is null) return null;

        return new BookResponse
        {
            Id = book.Id,
            LibraryId = book.LibraryId,
            Isbn = book.Isbn,
            Title = book.Title,
            Author = book.Author,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies
        };
    }
}