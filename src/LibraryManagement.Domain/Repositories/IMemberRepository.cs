using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Member>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Member member, CancellationToken cancellationToken);
    Task DeleteAsync(Member member, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}