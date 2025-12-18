namespace LibraryManagement.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Isbn { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Author { get; private set; } = null!;
    public int TotalCopies { get; private set; }
    public int AvailableCopies { get; private set; }

    public Guid LibraryId { get; private set; }
    public Library Library { get; private set; } = null!;

    private Book() { }

    public Book(
        string isbn,
        string title,
        string author,
        int totalCopies,
        Guid libraryId)
    {
        if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN is required");
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author is required");
        if (totalCopies <= 0) throw new ArgumentException("Total copies must be positive");
        if (libraryId == Guid.Empty) throw new ArgumentException("LibraryId is required");

        Id = Guid.NewGuid();
        Isbn = isbn;
        Title = title;
        Author = author;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
        LibraryId = libraryId;
    }

    public void Borrow()
    {
        if (AvailableCopies <= 0)
            throw new InvalidOperationException("No copies available.");

        AvailableCopies--;
    }

    public void Return()
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException("All copies already returned.");

        AvailableCopies++;
    }

    public void UpdateDetails(string isbn, string title, string author, int totalCopies)
    {
        if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN is required");
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author is required");
        if (totalCopies <= 0) throw new ArgumentException("Total copies must be positive");

        Isbn = isbn;
        Title = title;
        Author = author;
        TotalCopies = totalCopies;

        // Ensure AvailableCopies never exceeds TotalCopies
        if (AvailableCopies > TotalCopies)
            AvailableCopies = TotalCopies;
    }

}