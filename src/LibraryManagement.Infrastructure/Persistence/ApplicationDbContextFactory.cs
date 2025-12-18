using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence;

// Design-time factory for ApplicationDbContext
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Hardcoded connection string for design-time usage
        // Replace with your actual PostgreSQL connection string
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5433;Database=library_management;Username=myuser;Password=mypassword");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}