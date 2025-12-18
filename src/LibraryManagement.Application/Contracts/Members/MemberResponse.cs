namespace LibraryManagement.Application.Contracts.Members;

public sealed class MemberResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public DateTime RegisteredAt { get; init; }
}