namespace LibraryManagement.Application.Contracts.Libraries;
public class UpdateLibraryRequest
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}