using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;
public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Book>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Book book, CancellationToken cancellationToken);
    Task DeleteAsync(Book book, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
