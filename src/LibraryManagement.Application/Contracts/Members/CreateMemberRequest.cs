namespace LibraryManagement.Application.Contracts.Members;

public sealed class CreateMemberRequest
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
}