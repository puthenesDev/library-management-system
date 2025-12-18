namespace LibraryManagement.Application.Contracts.Libraries;
public sealed class LibraryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Address { get; init; } = null!;
}