using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;

public interface ILibraryRepository
{
    Task<Library?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Library library, CancellationToken cancellationToken);
    Task DeleteAsync(Library library, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}