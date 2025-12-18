using LibraryManagement.Application.Contracts.Books;
using MediatR;

namespace LibraryManagement.Application.Books.Queries;

public sealed record GetBooksQuery() : IRequest<IReadOnlyList<BookResponse>>;