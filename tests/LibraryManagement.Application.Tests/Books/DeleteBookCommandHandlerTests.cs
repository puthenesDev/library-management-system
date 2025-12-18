using LibraryManagement.Application.Books.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Books;

public class DeleteBookCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnTrue_WhenBookExists()
    {
        // Arrange
        var book = new Book("Isbn_1234567890", "Title", "Peter", 1, Guid.NewGuid());

        var repoMock = new Mock<IBookRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);
        repoMock
            .Setup(r => r.DeleteAsync(book, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteBookCommandHandler(repoMock.Object);
        var command = new DeleteBookCommand(book.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        repoMock.Verify(r => r.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(book, It.IsAny<CancellationToken>()), Times.Once);
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

        var handler = new DeleteBookCommandHandler(repoMock.Object);
        var command = new DeleteBookCommand(Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Never);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
