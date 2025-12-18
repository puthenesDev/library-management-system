using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Books.Commands;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
{
    private readonly IBookRepository _bookRepo;

    public UpdateBookCommandHandler(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepo.GetByIdAsync(request.Id, cancellationToken);
        if (book == null) return false;

        book.UpdateDetails(request.Isbn, request.Title, request.Author, request.TotalCopies);
        await _bookRepo.SaveChangesAsync(cancellationToken);

        return true;
    }
}