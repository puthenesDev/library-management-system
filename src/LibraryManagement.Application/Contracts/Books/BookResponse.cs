namespace LibraryManagement.Application.Contracts.Books;

public sealed class BookResponse
{
    public Guid Id { get; init; }
    public Guid LibraryId { get; init; }
    public string Isbn { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Author { get; init; } = null!;
    public int TotalCopies { get; init; }
    public int AvailableCopies { get; init; }
}