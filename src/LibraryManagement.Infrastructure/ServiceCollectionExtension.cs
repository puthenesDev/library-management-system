using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.Repositories.Books;
using LibraryManagement.Infrastructure.Repositories.Libraries;
using LibraryManagement.Infrastructure.Repositories.Loans;
using LibraryManagement.Infrastructure.Repositories.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();

        return services;
    }
}