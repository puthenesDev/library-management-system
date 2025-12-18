using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Libraries.Commands;

public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand, bool>
{
    private readonly ILibraryRepository _repository;

    public UpdateLibraryCommandHandler(ILibraryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
    {
        var library = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (library == null)
            return false;

        library.GetType().GetProperty("Name")?.SetValue(library, request.Name);
        library.GetType().GetProperty("Address")?.SetValue(library, request.Address);

        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}