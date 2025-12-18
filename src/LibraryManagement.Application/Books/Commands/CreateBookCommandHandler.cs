using LibraryManagement.Application.Contracts.Books;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Books.Commands;

public sealed class CreateBookCommandHandler
    : IRequestHandler<CreateBookCommand, BookResponse>
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(
        ILibraryRepository libraryRepository,
        IBookRepository bookRepository)
    {
        _libraryRepository = libraryRepository;
        _bookRepository = bookRepository;
    }

    public async Task<BookResponse> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
        var library = await _libraryRepository.GetByIdAsync(request.LibraryId, cancellationToken)
                      ?? throw new InvalidOperationException("Library not found.");

        var book = library.AddBook(request.Isbn, request.Title, request.Author, request.TotalCopies);

        await _bookRepository.AddAsync(book, cancellationToken);
        await _bookRepository.SaveChangesAsync(cancellationToken);

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
