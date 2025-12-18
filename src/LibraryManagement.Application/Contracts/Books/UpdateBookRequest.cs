namespace LibraryManagement.Application.Contracts.Books;

public class UpdateBookRequest
{
    public string Isbn { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public int TotalCopies { get; set; }
}