namespace LibraryManagement.Application.Contracts.Libraries;

public sealed class CreateLibraryRequest
{
    public string Name { get; init; } = null!;
    public string Address { get; init; } = null!;
}