namespace LibraryManagement.Domain.Entities;

public class Library
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;

    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    private Library() { }

    public Library(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address is required");

        Id = Guid.NewGuid();
        Name = name;
        Address = address;
    }

    public Book AddBook(string isbn, string title, string author, int totalCopies)
    {
        var book = new Book(isbn, title, author, totalCopies, Id);
        _books.Add(book);
        return book;
    }
}