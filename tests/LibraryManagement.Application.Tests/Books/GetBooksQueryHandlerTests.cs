using LibraryManagement.Application.Books.Queries;
using LibraryManagement.Application.Contracts.Books;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Books;

public class GetBooksQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnAllBooks()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book("1234567890", "Title1", "Author1", 1, Guid.NewGuid()),
            new Book("0987654321", "Title2", "Author2", 2, Guid.NewGuid())
        };

        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        var handler = new GetBooksQueryHandler(bookRepoMock.Object);

        // Act
        IReadOnlyList<BookResponse> result = await handler.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, b => b.Title == "Title1");
        Assert.Contains(result, b => b.Title == "Title2");

        // Assert
        bookRepoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
