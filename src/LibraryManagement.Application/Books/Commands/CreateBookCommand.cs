using LibraryManagement.Application.Contracts.Books;
using MediatR;

namespace LibraryManagement.Application.Books.Commands;
public sealed record CreateBookCommand(
    Guid LibraryId,
    string Isbn,
    string Title,
    string Author,
    int TotalCopies
) : IRequest<BookResponse>;
