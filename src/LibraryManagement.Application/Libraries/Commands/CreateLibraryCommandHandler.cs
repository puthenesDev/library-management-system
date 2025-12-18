using LibraryManagement.Application.Contracts.Libraries;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Libraries.Commands;

public sealed class CreateLibraryCommandHandler
    : IRequestHandler<CreateLibraryCommand, LibraryResponse>
{
    private readonly ILibraryRepository _libraryRepository;

    public CreateLibraryCommandHandler(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    public async Task<LibraryResponse> Handle(
        CreateLibraryCommand request,
        CancellationToken cancellationToken)
    {
        var library = new Library(request.Name, request.Address);

        await _libraryRepository.AddAsync(library, cancellationToken);

        return new LibraryResponse
        {
            Id = library.Id,
            Name = library.Name,
            Address = library.Address
        };
    }
}