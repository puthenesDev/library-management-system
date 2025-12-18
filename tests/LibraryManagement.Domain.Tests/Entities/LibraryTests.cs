using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Tests.Entities;

public class LibraryTests
{
    [Fact]
    public void AddBook_CreatesBookLinkedToLibrary()
    {
        var library = new Library("Central", "Main Street");
        var book = library.AddBook("1234567890", "Title", "Author", 3);

        Assert.Equal(library.Id, book.LibraryId);
        Assert.Contains(book, library.Books);
    }
}