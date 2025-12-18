using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Books.Commands;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepo;

    public DeleteBookCommandHandler(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepo.GetByIdAsync(request.Id, cancellationToken);
        if (book == null) return false;

        await _bookRepo.DeleteAsync(book, cancellationToken);
        await _bookRepo.SaveChangesAsync(cancellationToken);

        return true;
    }
}