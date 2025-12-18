using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories.Books;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Books.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken)
    {
        await _context.Books.AddAsync(book, cancellationToken);
    }

    public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Book book, CancellationToken cancellationToken)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
