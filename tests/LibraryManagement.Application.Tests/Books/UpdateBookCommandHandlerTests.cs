using LibraryManagement.Application.Books.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Books;
public class UpdateBookCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_UpdateBook_WhenBookExists()
    {
        // Arrange
        var book = new Book("Isbn_1234567890", "Title1", "Author1", 1, Guid.NewGuid());

        var repoMock = new Mock<IBookRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateBookCommandHandler(repoMock.Object);
        var command = new UpdateBookCommand(
            book.Id,
            "Isbn_29876543210",
            "Title2",
            "Author2",
            5
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal("Isbn_29876543210", book.Isbn);
        Assert.Equal("Title2", book.Title);
        Assert.Equal("Author2", book.Author);
        Assert.Equal(5, book.TotalCopies);

        repoMock.Verify(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var repoMock = new Mock<IBookRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        var handler = new UpdateBookCommandHandler(repoMock.Object);
        var command = new UpdateBookCommand(Guid.NewGuid(), "isbn", "title", "author", 1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);

        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
