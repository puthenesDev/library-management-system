using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories.Loans;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Loans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Loan loan, CancellationToken cancellationToken)
    {
        await _context.Loans.AddAsync(loan, cancellationToken);
    }

    public async Task DeleteAsync(Loan loan, CancellationToken cancellationToken)
    {
        _context.Loans.Remove(loan);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}