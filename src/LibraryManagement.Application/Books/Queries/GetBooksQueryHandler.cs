using LibraryManagement.Application.Contracts.Books;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Books.Queries;

public sealed class GetBooksQueryHandler
    : IRequestHandler<GetBooksQuery, IReadOnlyList<BookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IReadOnlyList<BookResponse>> Handle(
        GetBooksQuery request,
        CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);

        return books
            .Select(b => new BookResponse
            {
                Id = b.Id,
                LibraryId = b.LibraryId,
                Isbn = b.Isbn,
                Title = b.Title,
                Author = b.Author,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies
            })
            .ToList();
    }
}