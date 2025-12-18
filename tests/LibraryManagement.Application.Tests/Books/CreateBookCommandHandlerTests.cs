using LibraryManagement.Application.Books.Commands;
using LibraryManagement.Application.Contracts.Books;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Books;
public class CreateBookCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_AddBook_When_LibraryExists()
    {
        // Arrange
        var library = new Library("National Library", "City Centre");

        var libraryRepoMock = new Mock<ILibraryRepository>();
        libraryRepoMock
            .Setup(r => r.GetByIdAsync(library.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(library);

        var bookRepoMock = new Mock<IBookRepository>();
        bookRepoMock
            .Setup(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        bookRepoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateBookCommandHandler(libraryRepoMock.Object, bookRepoMock.Object);
        var cmd = new CreateBookCommand(library.Id, "Isbn_1", "Title_1", "Author", 2);

        // Act
        BookResponse response = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Title_1", response.Title);

        bookRepoMock.Verify(r => r.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
        bookRepoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}