using LibraryManagement.Application.Members.Commands;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Application.Tests.Members;

public class DeleteMemberCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnTrue_WhenMemberExists()
    {
        // Arrange
        var member = new Member("David", "david@gmail.com");

        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);
        repoMock
            .Setup(r => r.DeleteAsync(member, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        repoMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteMemberCommandHandler(repoMock.Object);
        var command = new DeleteMemberCommand(member.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
        repoMock.Verify(r => r.GetByIdAsync(member.Id, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(member, It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse_WhenMemberDoesNotExist()
    {
        // Arrange
        var repoMock = new Mock<IMemberRepository>();
        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member?)null);

        var handler = new DeleteMemberCommandHandler(repoMock.Object);
        var command = new DeleteMemberCommand(Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
        repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repoMock.Verify(r => r.DeleteAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()), Times.Never);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}