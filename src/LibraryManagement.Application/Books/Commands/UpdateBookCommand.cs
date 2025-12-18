using MediatR;

namespace LibraryManagement.Application.Books.Commands;
public record UpdateBookCommand(Guid Id, string Isbn, string Title, string Author, int TotalCopies) : IRequest<bool>;
public record DeleteBookCommand(Guid Id) : IRequest<bool>;