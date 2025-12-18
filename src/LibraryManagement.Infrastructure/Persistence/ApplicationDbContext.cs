using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Library> Libraries => Set<Library>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Library>(builder =>
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(200);
            builder.Property(l => l.Address).IsRequired().HasMaxLength(300);

            builder.HasMany(l => l.Books)
                .WithOne(b => b.Library)
                .HasForeignKey(b => b.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Book>(builder =>
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Isbn).IsRequired().HasMaxLength(13);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Author).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Member>(builder =>
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(200);
        });

        ConfigureLoan(modelBuilder.Entity<Loan>());
    }

    private static void ConfigureLoan(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(l => l.Id);

        builder.HasOne(l => l.Book)
            .WithMany()
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Member)
            .WithMany()
            .HasForeignKey(l => l.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}