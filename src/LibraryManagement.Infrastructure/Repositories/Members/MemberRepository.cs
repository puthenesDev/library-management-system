using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories.Members;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Members.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Member>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Members.AsNoTracking().ToListAsync(cancellationToken);

    public async Task AddAsync(Member member, CancellationToken cancellationToken) =>
        await _context.Members.AddAsync(member, cancellationToken);

    public async Task DeleteAsync(Member member, CancellationToken cancellationToken)
    {
        _context.Members.Remove(member);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);
}