using LibraryManagement.Application.Contracts.Libraries;
using LibraryManagement.Application.Libraries.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Libraries;

public class CreateLibraryCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_AddLibrary()
    {
        // Arrange
        var repoMock = new Mock<ILibraryRepository>();

        repoMock
            .Setup(r => r.AddAsync(It.IsAny<Library>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateLibraryCommandHandler(repoMock.Object);

        var cmd = new CreateLibraryCommand("Central Library", "Main Street");

        // Act
        LibraryResponse response = await handler.Handle(cmd, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Central Library", response.Name);
        repoMock.Verify(r => r.AddAsync(It.IsAny<Library>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
