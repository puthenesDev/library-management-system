namespace LibraryManagement.Application.Contracts.Members;

public class UpdateMemberRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}