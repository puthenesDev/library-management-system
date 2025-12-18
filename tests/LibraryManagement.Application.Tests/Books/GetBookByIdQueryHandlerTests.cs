using LibraryManagement.Application.Books.Queries;
using LibraryManagement.Application.Contracts.Books;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Books;
public class GetBookByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnBook_WhenExists()
    {
        // Arrange
        var book = new Book("1234567890", "Title", "Author", 1, Guid.NewGuid());

        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock
            .Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        var handler = new GetBookByIdQueryHandler(bookRepoMock.Object);

        // Act
        BookResponse? result = await handler.Handle(new GetBookByIdQuery(book.Id), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result!.Id);
        Assert.Equal("Title", result.Title);

        // Assert
        bookRepoMock.Verify(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
}