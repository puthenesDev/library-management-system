using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Loan loan, CancellationToken cancellationToken);
    Task DeleteAsync(Loan loan, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}