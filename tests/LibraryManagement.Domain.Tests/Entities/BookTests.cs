using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Tests.Entities;

public class BookTests
{
    [Fact]
    public void Borrow_Decreases_AvailableCopies()
    {
        var book = new Book("1234567890", "Title", "Author", 2, Guid.NewGuid());
        book.Borrow();
        Assert.Equal(1, book.AvailableCopies);
    }

    [Fact]
    public void Borrow_Throws_When_NoCopiesAvailable()
    {
        var book = new Book("1234567890", "Title", "Author", 1, Guid.NewGuid());
        book.Borrow();
        Assert.Throws<InvalidOperationException>(() => book.Borrow());
    }

    [Fact]
    public void Return_Increases_AvailableCopies()
    {
        var book = new Book("1234567890", "Title", "Author", 2, Guid.NewGuid());
        book.Borrow();
        book.Return();
        Assert.Equal(2, book.AvailableCopies);
    }
}