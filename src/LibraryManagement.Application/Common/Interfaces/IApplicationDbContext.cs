using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Common.Interfaces;
//unused
public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Member> Members { get; }
    DbSet<Loan> Loans { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}