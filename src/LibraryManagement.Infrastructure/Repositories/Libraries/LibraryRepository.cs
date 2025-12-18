using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories.Libraries;

public class LibraryRepository : ILibraryRepository
{
    private readonly ApplicationDbContext _context;

    public LibraryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Library?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Libraries
            .Include(l => l.Books)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task AddAsync(Library library, CancellationToken cancellationToken)
    {
        await _context.Libraries.AddAsync(library, cancellationToken);
    }

    public async Task DeleteAsync(Library library, CancellationToken cancellationToken)
    {
        _context.Libraries.Remove(library);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}