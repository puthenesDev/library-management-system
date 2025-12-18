using LibraryManagement.Application.Libraries.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Libraries;
public class DeleteLibraryCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnTrue_WhenLibraryExists()
    {
        // Arrange
        var library = new Library("National Library", "City Centre");

        var repoMock = new Mock<ILibraryRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(library.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(library);
        repoMock
            .Setup(r => r.DeleteAsync(library, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteLibraryCommandHandler(repoMock.Object);
        var command = new DeleteLibraryCommand(library.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        repoMock.Verify(r => r.GetByIdAsync(library.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(library, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse_WhenLibraryDoesNotExist()
    {
        // Arrange
        var repoMock = new Mock<ILibraryRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Library?)null);

        var handler = new DeleteLibraryCommandHandler(repoMock.Object);
        var command = new DeleteLibraryCommand(Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(It.IsAny<Library>(), It.IsAny<CancellationToken>()), Times.Never);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}

