namespace LibraryManagement.Application.Contracts.Books;
public sealed class CreateBookRequest
{
    public Guid LibraryId { get; init; }
    public string Isbn { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Author { get; init; } = null!;
    public int TotalCopies { get; init; }
}
