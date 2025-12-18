using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Libraries.Commands;

public class DeleteLibraryCommandHandler : IRequestHandler<DeleteLibraryCommand, bool>
{
    private readonly ILibraryRepository _repository;

    public DeleteLibraryCommandHandler(ILibraryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
    {
        var library = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (library == null)
            return false;

        await _repository.DeleteAsync(library, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}