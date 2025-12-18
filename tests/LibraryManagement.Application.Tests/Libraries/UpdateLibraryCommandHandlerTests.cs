using LibraryManagement.Application.Libraries.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Libraries;
public class UpdateLibraryCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_UpdateLibrary_WhenLibraryExists()
    {
        // Arrange
        var library = new Library("Name1", "Address1");

        var repoMock = new Mock<ILibraryRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(library.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(library);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateLibraryCommandHandler(repoMock.Object);
        var command = new UpdateLibraryCommand(library.Id, "Name2", "Address2");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        Assert.Equal("Name2", library.Name);
        Assert.Equal("Address2", library.Address);

        repoMock.Verify(r => r.GetByIdAsync(library.Id, It.IsAny<CancellationToken>()), Times.Once);
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

        var handler = new UpdateLibraryCommandHandler(repoMock.Object);
        var command = new UpdateLibraryCommand(Guid.NewGuid(), "Name1", "Address1");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);

        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
